using System.ComponentModel.DataAnnotations;

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
