using System;
using AccountabilityLib;

namespace BengasBowlingLib
{
    public class Series
    {
        public int SeriesId { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public Guid PartyId { get; set; }
        public Party Party { get; set; }
        public int Score { get; set; }
    }
}
