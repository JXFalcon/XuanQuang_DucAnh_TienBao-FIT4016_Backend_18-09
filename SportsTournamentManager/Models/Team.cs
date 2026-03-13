using System.Collections.Generic;

namespace SportsTournamentManager.Models {
    public class Team {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Country { get; set; } = string.Empty;

        public ICollection<Player> Players { get; set; } = new List<Player>();

        // Quan hệ 1-1 với Coach
        public Coach? Coach { get; set; }
    }
}