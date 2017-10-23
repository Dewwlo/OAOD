using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using AccountabilityLib;

namespace BengansBowlingModelsLib
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
        public Party MatchWinner
        {
            get
            {
                return Series.GroupBy(s => s.Party).Select(g => new { g.Key, value = g.Sum(s => s.Score) })
                   .OrderByDescending(o => o.value).Select(s => s.Key).FirstOrDefault();
            }
        }
        public int TimePeriodId { get; set; }
        public TimePeriod TimePeriod { get; set; }
    }
}
