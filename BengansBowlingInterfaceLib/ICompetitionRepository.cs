using System.Collections.Generic;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface ICompetitionRepository
    {
        void Create(string name, string type);
        void Create(string name, string type, decimal winnerPriceSum);
        void AddCompetitor(int competitionId, Player player);
        void AddMatch(int competitionId ,Match match);
        List<Match> GetMatches(int competitionId);
        List<Competition> All();
        Player Winner(int competitionId);
        List<Player> GetCompetitors(int competitionId);
    }
}
