using System;
using System.Linq;
using AccountabilityInterfacesLib;
using AccountabilityLib;
using BengansBowlingDbLib;
using BengansBowlingLib;
using BengansBowlingModelsLib;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BengansBowlingIntergrationTestsLib
{
    public class BengansBowlingTests
    {
        private readonly BengansBowlingContext _context;
        private readonly BowlingManager _sut;
        
        public BengansBowlingTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BengansBowlingContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            //optionsBuilder.UseSqlServer(
            //    "Server=(localdb)\\mssqllocaldb;Database=BengansBowling;Trusted_Connection=True;MultipleActiveResultSets=True");
            _context = new BengansBowlingContext(optionsBuilder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _sut = new BowlingManager(
                new SqlPartyRepository(_context), 
                new SqlCompetitionRepository(_context), 
                new SqlMatchRepository(_context),
                new SqlTimePeriodRepository(_context),
                new SqlSeriesRepository(_context));
        }

        [Fact]
        public void CreateParties()
        {
            _sut.CreateParty("Sture Sturesson", "7701012345");
            _sut.CreateParty("Greger Gregersson", "7801012345");
            Assert.Equal(2, _sut.GetAllParties.Count);
        }

        [Fact]
        public void CreateMatch()
        {
            _sut.CreateParty("Sture Sturesson", "7701012345");
            _sut.CreateParty("Greger Gregersson", "7801012345");
            _sut.CreateTimePeriod(DateTime.Now, DateTime.Now.AddHours(2));
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(), 
                new Lane { LaneId = 1, Name = "Bana 1" });

            Assert.Equal(2, _sut.GetMatchCompetitors(_context.Matches.FirstOrDefaultAsync().Id).Count);
        }

        [Fact]
        public void CreateCompetition()
        {
            _sut.CreateParty("Sture Sturesson", "7701012345");
            _sut.CreateParty("Greger Gregersson", "7801012345");
            _sut.CreateCompetition("Tävling1", 2000M);
            _sut.CreateCompetition("Tävling2");
            _sut.CreateCompetition("Tävling3", 5000M);
            _sut.GetAllParties.ForEach(p => _sut.AddPlayerToCompetition(_context.Competitions.FirstOrDefault().CompetitionId, p));

            Assert.Equal(3, _sut.GetAllCompetitions().Count);
            Assert.Equal(2, _sut.GetAllCompetitionCompetitors(_context.Competitions.FirstOrDefault().CompetitionId).Count);
            Assert.Equal(0, _sut.GetAllCompetitionCompetitors(_context.Competitions.OrderByDescending(c => c.CompetitionId).FirstOrDefault().CompetitionId).Count);
            Assert.Equal(2000M, _context.Competitions.FirstOrDefault().WinnerPriceSum);
        }

        [Fact]
        public void SetupCompetitionWithMatchesAndCompetitors()
        {
            _sut.CreateParty("Sture Sturesson", "7701012345");
            _sut.CreateParty("Greger Gregersson", "7801012345");
            _sut.CreateCompetition("Tävling1", 2000M);

            var competitionId = _context.Competitions.FirstOrDefault().CompetitionId;
            _sut.GetAllParties.ForEach(p => _sut.AddPlayerToCompetition(competitionId, p));
            _sut.CreateTimePeriod(DateTime.Now, DateTime.Now.AddHours(2));
            _sut.CreateTimePeriod(DateTime.Now.AddHours(2), DateTime.Now.AddHours(4));

            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(),
                new Lane { LaneId = 1, Name = "Bana 1" });
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(),
                new Lane { LaneId = 2, Name = "Bana 2" },
                _context.Competitions.FirstOrDefault());
            _sut.AddMatchToCompetition(competitionId, _sut.GetAllMatches().FirstOrDefault());

            var competition = _context.Competitions.FirstOrDefault();

            Assert.Equal(2, competition.Matches.Count);
            Assert.Equal(2, competition.Matches.FirstOrDefault().Players.Count);
        }

        [Fact]
        public void SeriesTests()
        {
            var score = _sut.GetSeriesScore;
            Assert.Equal(300, score);
        }
    }
}
