using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingInterfaceLib;
using BengansBowlingModelsLib;

namespace BengansBowlingLib
{
    public class SqlCompetitionRepository : ICompetitionRepository
    {
        public void Create(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Create(string name, decimal winnerPriceSum)
        {
            throw new System.NotImplementedException();
        }

        public List<Competition> GetAllCompetitions()
        {
            throw new System.NotImplementedException();
        }

        public List<Competition> GetCompetitions(string term)
        {
            throw new System.NotImplementedException();
        }

        public Party GetCompetitionWinner(int competitionId)
        {
            throw new System.NotImplementedException();
        }

        public List<Party> GetCompetitors(int competitionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
