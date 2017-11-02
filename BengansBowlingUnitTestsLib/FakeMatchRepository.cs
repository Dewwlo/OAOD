using System;
using System.Collections.Generic;
using System.Text;
using AccountabilityLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeMatchRepository : IMatchRepository
    {
        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane)
        {
            throw new NotImplementedException();
        }

        public void Create(List<Player> players, TimePeriod timePeriod, Lane lane, Competition competition)
        {
            throw new NotImplementedException();
        }

        public List<Match> All()
        {
            throw new NotImplementedException();
        }

        public List<Player> GetCompetitors(int matchId)
        {
            throw new NotImplementedException();
        }

        public Player Winner(int matchId)
        {
            throw new NotImplementedException();
        }

        public Player YearChampion(int year)
        {
            throw new NotImplementedException();
        }
    }
}
