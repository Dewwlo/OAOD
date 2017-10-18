using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccountabilityLib;

namespace BengasBowlingLib
{
    public class Lane
    {
        [Key]
        public int LaneId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
