namespace SportsTournamentManager.Models {
    public class Coach {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Nationality { get; set; }

        // Mỗi Coach gắn với một Team
        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}