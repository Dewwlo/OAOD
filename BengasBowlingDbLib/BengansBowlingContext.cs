using AccountabilityLib;
using BengansBowlingModelsLib;
using Microsoft.EntityFrameworkCore;

namespace BengansBowlingDbLib
{
    public class BengansBowlingContext : DbContext
    {
        private DbContextOptions<BengansBowlingContext> options;

        public BengansBowlingContext(DbContextOptions<BengansBowlingContext> options) : base(options)
        {
            this.options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerMatch>()
                .HasKey(pm => new { pm.MatchId, pm.PlayerId });

            builder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Player)
                .WithMany(m => m.Matches)
                .HasForeignKey(p => p.PlayerId);

            builder.Entity<PlayerMatch>()
                .HasOne(pm => pm.Match)
                .WithMany(p => p.Players)
                .HasForeignKey(m => m.MatchId);

            builder.Entity<PlayerCompetition>()
                .HasKey(pc => new { pc.CompetitionId, pc.PlayerId });

            builder.Entity<PlayerCompetition>()
                .HasOne(pc => pc.Player)
                .WithMany(c => c.Competitions)
                .HasForeignKey(p => p.PlayerId);

            builder.Entity<PlayerCompetition>()
                .HasOne(pc => pc.Competition)
                .WithMany(p => p.Players)
                .HasForeignKey(c => c.CompetitionId);
                
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerMatch> PlayerMatches { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<PlayerCompetition> PlayerCompetitions { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Series> Series { get; set; }
    } 
}
