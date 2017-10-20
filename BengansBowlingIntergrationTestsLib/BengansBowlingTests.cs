using System;
using System.Linq;
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
        
        public BengansBowlingTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BengansBowlingContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            //optionsBuilder.UseSqlServer(
            //    "Server=(localdb)\\mssqllocaldb;Database=BengansBowling;Trusted_Connection=True;MultipleActiveResultSets=True");
            _context = new BengansBowlingContext(optionsBuilder.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void CreateParties()
        {
            var sut = new BowlingManager(new SqlPartyRepository(_context), new SqlCompetitionRepository(_context), new SqlMatchRepository(_context));
            sut.CreateParty("Sture Sturesson", "7701012345");
            sut.CreateParty("Greger Gregersson", "7801012345");
            Assert.Equal(2, sut.GetAllParties.Count);
        }

        [Fact]
        public void CreateMatch()
        {
            var sut = new BowlingManager(new SqlPartyRepository(_context), new SqlCompetitionRepository(_context), new SqlMatchRepository(_context));

            sut.CreateParty("Sture Sturesson", "7701012345");
            sut.CreateParty("Greger Gregersson", "7801012345");

            sut.CreateMatch(
                sut.GetAllParties,
                new TimePeriod { TimePeriodId = 1, FromDate = DateTime.Now, ToDate = DateTime.Now.AddHours(2) }, 
                new Lane { LaneId = 1, Name = "Bana 1" });

            Assert.Equal(2, sut.GetMatchCompetitors(_context.Matches.FirstOrDefaultAsync().Id).Count);
        }

        [Fact]
        public void CreateCompetition()
        {
            var sut = new BowlingManager(new SqlPartyRepository(_context), new SqlCompetitionRepository(_context), new SqlMatchRepository(_context));

            sut.CreateParty("Sture Sturesson", "7701012345");
            sut.CreateParty("Greger Gregersson", "7801012345");
            sut.CreateCompetition("Tävling1", 2000M);
            sut.CreateCompetition("Tävling2");
            sut.CreateCompetition("Tävling3", 5000M);
            sut.GetAllParties.ForEach(p => sut.AddPlayerToCompetition(_context.Competitions.FirstOrDefault().CompetitionId, p));

            Assert.Equal(3, sut.GetAllCompetitions().Count);
            Assert.Equal(2, sut.GetAllCompetitionCompetitors(_context.Competitions.FirstOrDefault().CompetitionId).Count);
            Assert.Equal(0, sut.GetAllCompetitionCompetitors(_context.Competitions.OrderByDescending(c => c.CompetitionId).FirstOrDefault().CompetitionId).Count);
            Assert.Equal(2000M, _context.Competitions.FirstOrDefault().WinnerPriceSum);
        }
    }
}
