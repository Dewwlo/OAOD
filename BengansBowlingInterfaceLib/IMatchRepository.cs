using System.Collections.Generic;
using AccountabilityLib;

namespace BengansBowlingInterfaceLib
{
    public interface IMatchRepository
    {
        void Create(List<Party> parties, TimePeriod timePeriod, int laneId);
        void Create(List<Party> parties, TimePeriod timePeriod, int laneId, int competitionId);
        Party GetMatchWinner(int matchId);

    }
}
