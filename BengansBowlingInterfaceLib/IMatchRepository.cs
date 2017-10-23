using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingModelsLib;

namespace BengansBowlingInterfaceLib
{
    public interface IMatchRepository
    {
        void Create(List<Party> parties, TimePeriod timePeriod, Lane lane);
        void Create(List<Party> parties, TimePeriod timePeriod, Lane lane, Competition competition);
        List<Match> All();
        List<Party> GetCompetitors(int matchId);
        Party Winner(int matchId);
        Party YearChampion(int year);
    }
}
