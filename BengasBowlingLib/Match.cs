using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccountabilityLib;

namespace BengasBowlingLib
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        public List<Party> Players { get; set; }
        public List<Series> Series { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        [Required]
        public int LaneId { get; set; }
        [Required]
        public Lane Lane { get; set; }
        public Party MatchWinner { get; set; }
        public int TimePeriodId { get; set; }
        public TimePeriod TimePeriod { get; set; }
    }
}
