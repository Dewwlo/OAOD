using System;
using System.Collections.Generic;
using System.Linq;
using AccountabilityLib;
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
                new FakeSeriesRepository(),
                new FakeLaneRepository());
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

            Assert.Equal(2, _sut.GetMatchCompetitors(_sut.GetAllMatches().FirstOrDefault().MatchId).Count);
        }
    }
}
