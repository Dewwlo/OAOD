using System.ComponentModel.DataAnnotations;
using AccountabilityLib;

namespace BengasBowlingLib
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        [Required]
        public Party PlayerHome { get; set; }
        [Required]
        public Party PlayerAway { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        [Required]
        public int LaneId { get; set; }
        [Required]
        public Lane Lane { get; set; }
        public Party MatchWinner { get; set; }
    }
}
