using System.Collections.Generic;
using AccountabilityLib;

namespace BengansBowlingModelsLib
{
    public class Player : Party
    {
        public List<Series> Series { get; set; }
        public List<PlayerMatch> Matches { get; set; }
        public List<PlayerCompetition> Competitions { get; set; }
    }
}
