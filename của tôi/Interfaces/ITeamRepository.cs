using qlgiaidau.Models;
using System.Collections.Generic;

namespace qlgiaidau.Interfaces
{
    public interface ITeamRepository
    {
        void AddTeam(Team team);
        void UpdateTeam(Team team);
        void DeleteTeam(int teamId);
        Team? GetTeamById(int teamId);
        List<Team> GetAllTeams();

        void SeedSampleTeam();
        void SeedSampleMatches();
        void SeedSampleNotifications();
        void SeedSamplePlayerStats();
        void SeedSampleMatchResults();
        void SeedSampleUsers();
    }

}