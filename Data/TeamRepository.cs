using qlgiaidau.Interfaces;
using qlgiaidau.Models;
using System.Collections.Generic;
using System.Linq;

namespace qlgiaidau.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddTeam(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
        }

        public void UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            _context.SaveChanges();
        }

        public void DeleteTeam(int teamId)
        {
            var team = _context.Teams.Find(teamId);
            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
        }

        public Team? GetTeamById(int teamId) => _context.Teams.Find(teamId);

        public List<Team> GetAllTeams() => _context.Teams.ToList();

        // Seed dữ liệu mẫu cho Teams
        public void SeedSampleTeam()
        {
            if (_context.Teams.Any()) return;

            var teams = new List<Team>
            {
                new Team { Name = "FC Hà Nội", LogoUrl = "hanoi.png", Description = "Đội bóng thủ đô", Wins = 10, Losses = 2 },
                new Team { Name = "CLB Công An Hà Nội", LogoUrl = "conganhanoi.png", Description = "Công an danh dàn", Wins = 10, Losses = 2 },
                new Team { Name = "FC Thanh Hóa", LogoUrl = "thanhhoa.png", Description = "Đội bóng miền Trung", Wins = 5, Losses = 7 },
                new Team { Name = "CLB Hoàng Anh Gia Lai", LogoUrl = "hoanganhgiailai.png", Description = "Hoàng Anh Gia Lai", Wins = 5, Losses = 7 },
                new Team { Name = "FC Thép Xanh Nam Định", LogoUrl = "namdinh.png", Description = "Đội bóng thành Nam", Wins = 6, Losses = 6 },
                new Team { Name = "Sông Lam Nghệ An", LogoUrl = "songlamnghean.png", Description = "Tôi là Người Nghệ An", Wins = 7, Losses = 5 }
            };

            _context.Teams.AddRange(teams);
            _context.SaveChanges();
        }

        // Seed dữ liệu mẫu cho Matches
        public void SeedSampleMatches()
        {
            if (_context.Matches.Any()) return;

            var matches = new List<Match>
            {
                new Match { TeamAId = 1, TeamBId = 2, MatchDate = DateTime.Now.AddDays(-7), Location = "Sân Hàng Đẫy" },
                new Match { TeamAId = 3, TeamBId = 4, MatchDate = DateTime.Now.AddDays(-3), Location = "Sân Thiên Trường" },
                new Match { TeamAId = 5, TeamBId = 1, MatchDate = DateTime.Now.AddDays(-1), Location = "Sân Vinh" }
            };

            _context.Matches.AddRange(matches);
            _context.SaveChanges();
        }

        // Seed dữ liệu mẫu cho MatchResults
        public void SeedSampleMatchResults()
        {
            if (_context.MatchResults.Any()) return;

            var results = new List<MatchResult>
            {
                new MatchResult { MatchId = 1, TeamAScore = 2, TeamBScore = 1 },
                new MatchResult { MatchId = 2, TeamAScore = 0, TeamBScore = 3 },
                new MatchResult { MatchId = 3, TeamAScore = 1, TeamBScore = 1 }
            };

            _context.MatchResults.AddRange(results);
            _context.SaveChanges();
        }

        // Seed dữ liệu mẫu cho PlayerStats
        public void SeedSamplePlayerStats()
        {
            if (_context.PlayerStat.Any()) return;

            var stats = new List<PlayerStat>
            {
                new PlayerStat { PlayerName = "Nguyễn Văn A", Goals = 5, Assists = 2, TeamId = 1 },
                new PlayerStat { PlayerName = "Trần Văn B", Goals = 3, Assists = 4, TeamId = 2 },
                new PlayerStat { PlayerName = "Lê Văn C", Goals = 2, Assists = 1, TeamId = 3 },
                new PlayerStat { PlayerName = "Phạm Văn D", Goals = 4, Assists = 3, TeamId = 4 }
            };

            _context.PlayerStat.AddRange(stats);
            _context.SaveChanges();
        }

        // Seed dữ liệu mẫu cho Users
        public void SeedSampleUsers()
        {
            if (_context.Users.Any()) return;

            var users = new List<User>
            {
                new User { UserName = "admin", PasswordHash = "ducanhtraidep", Role = UserRole.Admin },
                new User { UserName = "viewer", PasswordHash = "conbac", Role = UserRole.Viewer },
                new User { UserName = "coach", PasswordHash = "phamduyanh", Role = UserRole.Coach }
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        // Seed dữ liệu mẫu cho Notifications
        public void SeedSampleNotifications()
        {
            if (_context.Notifications.Any()) return;

            var notifications = new List<Notification>
            {
                new Notification { Message = "Trận đấu Công An Hà Nội vs Thanh Hóa bắt đầu lúc 19h", CreatedAt = DateTime.Now },
                new Notification { Message = "Kết quả: Hoàng Anh Gia Lai hòa Nam Định 1-1", CreatedAt = DateTime.Now },
                new Notification { Message = "Sông Lam Nghệ An chuẩn bị gặp Công An Hà Nội", CreatedAt = DateTime.Now }
            };

            _context.Notifications.AddRange(notifications);
            _context.SaveChanges();
        }
    }
}