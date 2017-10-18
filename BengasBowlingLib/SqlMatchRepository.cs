using System.Collections.Generic;
using AccountabilityLib;
using BengansBowlingInterfaceLib;

namespace BengansBowlingLib
{
    public class SqlMatchRepository : IMatchRepository
    {
        public void Create(List<Party> parties, TimePeriod timePeriod, int laneId)
        {
            throw new System.NotImplementedException();
        }

        public void Create(List<Party> parties, TimePeriod timePeriod, int laneId, int competitionId)
        {
            throw new System.NotImplementedException();
        }

        public Party GetMatchWinner(int matchId)
        {
            throw new System.NotImplementedException();
        }
    }
}
