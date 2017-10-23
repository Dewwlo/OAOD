using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface IMatchRepository
    {
        void Create(List<Player> players, TimePeriod timePeriod, Lane lane);
        void Create(List<Player> players, TimePeriod timePeriod, Lane lane, Competition competition);
        List<Match> All();
        List<Player> GetCompetitors(int matchId);
        Player Winner(int matchId);
        Player YearChampion(int year);
    }
}
