using Microsoft.EntityFrameworkCore;
using SportsTournamentManager.Models;

namespace SportsTournamentManager.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TournamentSponsor> TournamentSponsors { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key cho TournamentSponsor
            modelBuilder.Entity<TournamentSponsor>()
                .HasKey(ts => new { ts.TournamentId, ts.SponsorId });

            modelBuilder.Entity<TournamentSponsor>()
                .HasOne(ts => ts.Tournament)
                .WithMany(t => t.TournamentSponsors)
                .HasForeignKey(ts => ts.TournamentId);

            modelBuilder.Entity<TournamentSponsor>()
                .HasOne(ts => ts.Sponsor)
                .WithMany(s => s.TournamentSponsors)
                .HasForeignKey(ts => ts.SponsorId);

            // Match ↔ TeamA
            modelBuilder.Entity<Match>()
                .HasOne(m => m.TeamA)
                .WithMany()
                .HasForeignKey(m => m.TeamAId)
                .OnDelete(DeleteBehavior.NoAction);

            // Match ↔ TeamB
            modelBuilder.Entity<Match>()
                .HasOne(m => m.TeamB)
                .WithMany()
                .HasForeignKey(m => m.TeamBId)
                .OnDelete(DeleteBehavior.NoAction);

            // Match ↔ Tournament
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Tournament)
                .WithMany(t => t.Matches)
                .HasForeignKey(m => m.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tournament ↔ Discipline
            modelBuilder.Entity<Tournament>()
                .HasOne(t => t.Discipline)
                .WithMany(d => d.Tournaments)
                .HasForeignKey(t => t.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tournament ↔ Venue
            modelBuilder.Entity<Tournament>()
                .HasOne(t => t.Venue)
                .WithMany(v => v.Tournaments)
                .HasForeignKey(t => t.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Player ↔ Team
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Coach ↔ Team (1-1)
            modelBuilder.Entity<Coach>()
                .HasOne(c => c.Team)
                .WithOne(t => t.Coach)
                .HasForeignKey<Coach>(c => c.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}