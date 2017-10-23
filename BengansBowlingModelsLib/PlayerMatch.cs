using System;

namespace BengansBowlingModelsLib
{
    public class PlayerMatch
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
    }
}
