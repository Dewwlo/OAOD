using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccountabilityLib;

namespace BengansBowlingModelsLib
{
    public class Competition
    {
        [Key]
        public int CompetitionId { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal WinnerPriceSum { get; set; }
        public List<Party> Competitors { get; set; }
        public List<Match> Matches { get; set; }
    }
}
