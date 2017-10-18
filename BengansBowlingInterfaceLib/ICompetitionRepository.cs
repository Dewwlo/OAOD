using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface ICompetitionRepository
    {
        void Create(string name);
        void Create(string name, decimal winnerPriceSum);
        List<Competition> GetAllCompetitions();
        List<Competition> GetCompetitions(string term);
        Party GetCompetitionWinner(int competitionId);
        List<Party> GetCompetitors(int competitionId);
    }
}
