using System;
using System.Collections.Generic;
using System.Text;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingUnitTestsLib
{
    public class FakeCompetitionRepository : ICompetitionRepository
    {
        public void Create(string name, string type)
        {
            throw new NotImplementedException();
        }

        public void Create(string name, string type, decimal winnerPriceSum)
        {
            throw new NotImplementedException();
        }

        public void AddCompetitor(int competitionId, Player player)
        {
            throw new NotImplementedException();
        }

        public void AddMatch(int competitionId, Match match)
        {
            throw new NotImplementedException();
        }

        public List<Match> GetMatches(int competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Competition> All()
        {
            throw new NotImplementedException();
        }

        public Player Winner(int competitionId)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetCompetitors(int competitionId)
        {
            throw new NotImplementedException();
        }
    }
}
