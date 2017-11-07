using System;
using System.Collections.Generic;
using System.Linq;
using AccountabilityLib;
using BengansBowlingDbLib.FrameMemento;
using BengansBowlingLib;
using BengansBowlingModelsLib;
using Xunit;

namespace BengansBowlingUnitTestsLib
{
    public class BengansBowlingTests
    {
        private readonly BowlingManager _sut;

        public BengansBowlingTests()
        {
            _sut = new BowlingManager(
                new FakePlayerRepository(),
                new FakeCompetitionRepository(),
                new FakeMatchRepository(),
                new FakeTimePeriodRepository(),
                new FakeSeriesRepository(new FrameState()),
                new FakeLaneRepository());
        }

        [Fact]
        public void CreateMatchTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateLane("Bana 1");
            _sut.CreateTimePeriod(new DateTime(2017, 6, 20, 16, 0, 0), new DateTime(2017, 6, 20, 18, 0, 0));
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(),
                _sut.GetAllLanes().FirstOrDefault());

            Assert.Equal(2, _sut.GetMatchCompetitors(_sut.GetAllMatches().FirstOrDefault().MatchId).Count);
        }

        [Fact]
        public void CreateCompetitionTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateCompetition("Tävling1", "knockout", 2000M);
            _sut.CreateCompetition("Tävling3", "group", 5000M);
            _sut.GetAllParties.ForEach(p => _sut.AddPlayerToCompetition(_sut.GetAllCompetitions().FirstOrDefault().CompetitionId, p));

            var firstCompetition = _sut.GetAllCompetitions().FirstOrDefault();
            Assert.Equal("some rules", firstCompetition.Rules);
            Assert.Equal("Winner will advance to next round", firstCompetition.GameMode);
            Assert.Equal(2, _sut.GetAllCompetitions().Count);
            Assert.Equal(2, _sut.GetAllCompetitionCompetitors(firstCompetition.CompetitionId).Count);
            Assert.Equal(0, _sut.GetAllCompetitionCompetitors(_sut.GetAllCompetitions().OrderByDescending(c => c.CompetitionId).FirstOrDefault().CompetitionId).Count);
            Assert.Equal(2000M, firstCompetition.WinnerPriceSum);
        }

        [Fact]
        public void SetupCompetitionWithMatchesAndCompetitorsTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateCompetition("Tävling1", "knockout", 2000M);
            _sut.CreateLane("Bana 1");

            var competitionId = _sut.GetAllCompetitions().FirstOrDefault().CompetitionId;
            _sut.GetAllParties.ForEach(p => _sut.AddPlayerToCompetition(competitionId, p));
            _sut.CreateTimePeriod(new DateTime(2017, 6, 20, 16, 0, 0), new DateTime(2017, 6, 20, 18, 0, 0));
            _sut.CreateTimePeriod(new DateTime(2017, 6, 20, 18, 0, 0), new DateTime(2017, 6, 20, 20, 0, 0));

            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(),
                _sut.GetAllLanes().FirstOrDefault());
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(),
                _sut.GetAllLanes().FirstOrDefault());
            _sut.GetAllMatches().ForEach(m => _sut.AddMatchToCompetition(competitionId, m));

            var competition = _sut.GetAllCompetitions().FirstOrDefault();

            Assert.Equal(2, competition.Matches.Count);
            Assert.Equal(2, competition.Players.Count);
        }

        [Fact]
        public void MatchWinnerTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateLane("Bana 1");

            _sut.CreateTimePeriod(new DateTime(2017, 6, 20, 16, 0, 0), new DateTime(2017, 6, 20, 18, 0, 0));
            _sut.CreateMatch(
                _sut.GetAllParties,
                _sut.GetAllTimePeriods().FirstOrDefault(),
                _sut.GetAllLanes().FirstOrDefault());

            var playerOne =  _sut.GetAllParties.FirstOrDefault();
            var playerTwo = _sut.GetAllParties.Skip(1).FirstOrDefault();
            var match = _sut.GetAllMatches().FirstOrDefault();

            for (int series = 0; series < 3; series++)
            {
                _sut.CreateSeries(playerOne, match);
                _sut.AddSeriesScore(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 100);
                match.Series.Add(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault());
                _sut.CreateSeries(playerTwo, match);
                _sut.AddSeriesScore(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 101);
                match.Series.Add(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault());
            }
            

            Assert.Equal("7801012345", _sut.GetMatchWinner(match.MatchId).LegalId);
        }

        [Fact]
        public void YearChampionTest()
        {
            _sut.CreatePlayer("Sture Sturesson", "7701012345");
            _sut.CreatePlayer("Greger Gregersson", "7801012345");
            _sut.CreateTimePeriod(new DateTime(2017, 6, 20, 16, 0, 0), new DateTime(2017, 6, 20, 18, 0, 0));
            _sut.CreateLane("Bana 1");

            for (int match = 0; match < 3; match++)
            {
                _sut.CreateMatch(
                    _sut.GetAllParties,
                    _sut.GetAllTimePeriods().FirstOrDefault(),
                    _sut.GetAllLanes().FirstOrDefault());

                var playerOne = _sut.GetAllParties.FirstOrDefault();
                var playerTwo = _sut.GetAllParties.Skip(1).FirstOrDefault();
                var currentMatch = _sut.GetAllMatches().OrderByDescending(o => o.MatchId).FirstOrDefault();

                for (int i = 0; i < 3; i++)
                {
                    _sut.CreateSeries(playerOne, currentMatch);
                    _sut.AddSeriesScore(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 100);
                    currentMatch.Series.Add(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault());
                    _sut.CreateSeries(playerTwo, currentMatch);
                    _sut.AddSeriesScore(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault().SeriesId, 120);
                    currentMatch.Series.Add(_sut.GetAllSeries().OrderByDescending(o => o.SeriesId).FirstOrDefault());
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
