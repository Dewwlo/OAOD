using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BengansBowlingModelsLib
{
    public class Competition
    {
        [Key]
        public int CompetitionId { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal WinnerPriceSum { get; set; }
        public List<PlayerCompetition> Players { get; set; }
        public List<Match> Matches { get; set; }
    }
}
