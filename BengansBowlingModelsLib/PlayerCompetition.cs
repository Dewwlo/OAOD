using System;
using System.Collections.Generic;
using System.Text;

namespace BengansBowlingModelsLib
{
    public class PlayerCompetition
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
    }
}
