using System;
using System.Linq;
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
                new SqlPlayerRepository(_context), 
                new SqlCompetitionRepository(_context), 
                new SqlMatchRepository(_context),
                new SqlTimePeriodRepository(_context),
                new SqlSeriesRepository(_context, new FrameState()));
        }

        [Fact]
        public void CreateMatchTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateTimePeriod(DateTime.Now, DateTime.Now.AddHours(2));
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(), 
                new Lane { LaneId = 1, Name = "Bana 1" });

            Assert.Equal(2, _sut.GetMatchCompetitors(_context.Matches.FirstOrDefaultAsync().Id).Count);
        }

        [Fact]
        public void CreateCompetitionTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateCompetition("Tävling1", "knockout", 2000M);
            _sut.CreateCompetition("Tävling2", "group");
            _sut.CreateCompetition("Tävling3", "group", 5000M);
            _sut.GetAllParties.ForEach(p => _sut.AddPlayerToCompetition(_context.Competitions.FirstOrDefault().CompetitionId, p));

            Assert.Equal("some rules", _context.Competitions.FirstOrDefault().Rules);
            Assert.Equal("Winner will advance to next round", _context.Competitions.FirstOrDefault().GameMode);
            Assert.Equal(3, _sut.GetAllCompetitions().Count);
            Assert.Equal(2, _sut.GetAllCompetitionCompetitors(_context.Competitions.FirstOrDefault().CompetitionId).Count);
            Assert.Equal(0, _sut.GetAllCompetitionCompetitors(_context.Competitions.OrderByDescending(c => c.CompetitionId).FirstOrDefault().CompetitionId).Count);
            Assert.Equal(2000M, _context.Competitions.FirstOrDefault().WinnerPriceSum);
        }

        [Fact]
        public void SetupCompetitionWithMatchesAndCompetitorsTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateCompetition("Tävling1", "knockout", 2000M);

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
        public void MatchWinnerTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateTimePeriod(DateTime.Now, DateTime.Now.AddHours(2));
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(), 
                new Lane { LaneId = 1, Name = "Bana 1" });

            var playerOne = _context.Players.FirstOrDefault();
            var playerTwo = _context.Players.Skip(1).FirstOrDefault();
            var match = _context.Matches.FirstOrDefault();

            for (int series = 0; series < 3; series++)
            {
                _sut.CreateSeries(playerOne, match);
                _sut.AddSeriesScore(_context.Series.OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 100);
                _sut.CreateSeries(playerTwo, match);
                _sut.AddSeriesScore(_context.Series.OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 101);
            }

            Assert.Equal("7801012345", _sut.GetMatchWinner(match.MatchId).LegalId);
        }

        [Fact]
        public void YearChampionTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateTimePeriod(DateTime.Now, DateTime.Now.AddHours(2));

            for (int match = 0; match < 3; match++)
            {
                _sut.CreateMatch(
                    _sut.GetAllParties,
                    _sut.GetAllTimePeriods().FirstOrDefault(),
                    new Lane { LaneId = 1, Name = "Bana 1" });

                var playerOne = _context.Players.FirstOrDefault();
                var playerTwo = _context.Players.Skip(1).FirstOrDefault();
                var currentMatch = _context.Matches.OrderByDescending(o => o.MatchId).FirstOrDefault();

                for (int i = 0; i < 3; i++)
                {
                    _sut.CreateSeries(playerOne, currentMatch);
                    _sut.AddSeriesScore(_context.Series.OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 102);
                    _sut.CreateSeries(playerTwo, currentMatch);
                    _sut.AddSeriesScore(_context.Series.OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 103);
                }
            }

            Assert.Equal("7801012345", _sut.GetYearChampion(2017).LegalId);
        }

        [Fact]
        public void GetSeriesScoreTests()
        {
            var array = new[,] { { 10, 0 }, { 6, 4 }, { 3, 5 }, { 10, 0 }, { 10, 0 }, { 2, 0 }, { 5, 3 }, { 7, 3 }, { 8, 2 } };
            var frameTen = new[,] { { 4, 6, 10 } };
            var score = _sut.GetSeriesScore(array, frameTen);
            Assert.Equal(137, score);
        }

        [Fact]
        public void GetSeriesMaxScoreTests()
        {
            var array = new[,] { { 10, 0 }, { 10, 0 }, { 10, 0 }, { 10, 0 }, { 10, 0 }, { 10, 0 }, { 10, 0 }, { 10, 0 }, { 10, 0 } };
            var frameTen = new[,] { { 10, 10, 10 } };
            var score = _sut.GetSeriesScore(array, frameTen);
            Assert.Equal(300, score);
        }
    }
}
