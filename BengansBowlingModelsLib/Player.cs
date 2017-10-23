using System.Collections.Generic;
using AccountabilityLib;

namespace BengansBowlingModelsLib
{
    public class Player : Party
    {
        public List<PlayerMatch> Matches { get; set; }
    }
}
