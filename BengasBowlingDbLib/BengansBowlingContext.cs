using AccountabilityLib;
using BengasBowlingLib;
using Microsoft.EntityFrameworkCore;

namespace BengasBowlingDbLib
{
    public class BengansBowlingContext : DbContext
    {
        private DbContextOptions<BengansBowlingContext> options;

        public BengansBowlingContext(DbContextOptions<BengansBowlingContext> options) : base(options)
        {
            this.options = options;
        }

        public DbSet<Party> Parties { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Match> Matches { get; set; }
        //public DbSet<RoundSeries> RoundSeries { get; set; }
        public DbSet<Series> Series { get; set; }
    } 
}
