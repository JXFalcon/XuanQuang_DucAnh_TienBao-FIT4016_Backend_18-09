using Microsoft.EntityFrameworkCore;
using Web_BongDa.Models;

namespace Web_BongDa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu mẫu cho bảng Team
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "Manchester City",
                    Logo = "/image/Mc.jpg",
                    Played = 10, Won = 8, Drawn = 1, Lost = 1, Points = 25, YellowCards = 5, RedCards = 0
                },
                new Team
                {
                    Id = 2,
                    Name = "Arsenal",
                    Logo = "/Image/Ars.jpg",
                    Played = 10, Won = 7, Drawn = 2, Lost = 1, Points = 23, YellowCards = 12, RedCards = 1
                },
                new Team
                {
                    Id = 3,
                    Name = "Liverpool",
                    Logo = "/image/Liver.png",
                    Played = 10, Won = 6, Drawn = 3, Lost = 1, Points = 21, YellowCards = 8, RedCards = 0
                },
                new Team
                {
                    Id = 4,
                    Name = "Manchester United",
                    Logo = "/Image/Mu.jpg",
                    Played = 10, Won = 4, Drawn = 3, Lost = 3, Points = 15, YellowCards = 15, RedCards = 2
                }
            ); // Kết thúc HasData
        }
    }
}
