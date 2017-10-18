using System;
using System.ComponentModel.DataAnnotations;

namespace AccountabilityLib
{
    public class Party
    {
        [Key]
        public Guid PartyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LegalId { get; set; }
    }
}
