namespace SportsTournamentManager.Models {
    public class Player {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int TeamId { get; set; }
        
        public Team? Team { get; set; }
    }
}