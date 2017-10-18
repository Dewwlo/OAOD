using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccountabilityLib;

namespace BengansBowlingModelsLib
{
    public class Lane
    {
        [Key]
        public int LaneId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
