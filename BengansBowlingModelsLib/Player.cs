using System.Collections.Generic;
using AccountabilityLib;

namespace BengansBowlingModelsLib
{
    public class Player : Party
    {
        public List<Series> Series { get; set; } = new List<Series>();
        public List<PlayerMatch> Matches { get; set; } = new List<PlayerMatch>();
        public List<PlayerCompetition> Competitions { get; set; } = new List<PlayerCompetition>();
    }
}
