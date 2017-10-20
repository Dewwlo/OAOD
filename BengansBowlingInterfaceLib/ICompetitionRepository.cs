using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface ICompetitionRepository
    {
        void Create(string name);
        void Create(string name, decimal winnerPriceSum);
        void AddCompetitor(int competitionId, Party player);
        List<Competition> All();
        Party Winner(int competitionId);
        List<Party> GetCompetitors(int competitionId);
    }
}
