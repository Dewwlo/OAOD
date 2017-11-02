using System;
using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingModelsLib;
using Xunit;

namespace BengansBowlingUnitTestsLib
{
    public class BengansBowlingTests
    {
        [Fact]
        public void MatchWinnerTest()
        {
            var playerOne = new Player {Name = "Sture Sturesson", LegalId = "7701012345"};
            var playerTwo = new Player {Name = "Greger Gregersson", LegalId = "7801012345"};

            var match = new Match {MatchId = 1};
            match.Players.Add(new PlayerMatch { Player = playerOne, Match = match });
            match.Players.Add(new PlayerMatch { Player = playerTwo, Match = match });

            for (int series = 0; series < 3; series++)
            {
                var seriesPlayerOne = new Series {Score = 100, Player = playerOne, Match = match};
                var seriesPlayerTwo = new Series {Score = 120, Player = playerTwo, Match = match};
                match.Series.Add(seriesPlayerOne);
                match.Series.Add(seriesPlayerTwo);
                playerOne.Series.Add(seriesPlayerOne);
                playerTwo.Series.Add(seriesPlayerTwo);
            }
            
            Assert.Equal("7801012345", match.MatchWinner.LegalId);
        }
    }
}