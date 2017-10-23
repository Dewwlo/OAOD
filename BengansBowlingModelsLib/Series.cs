using System;

namespace BengansBowlingModelsLib
{
    public class Series
    {
        public int SeriesId { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public int Score { get; set; }
    }
}
