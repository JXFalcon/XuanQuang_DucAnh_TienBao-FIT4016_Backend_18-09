namespace SportsTournamentManager.Models
{
    public enum UserRole
    {
        Admin,
        Viewer
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // demo, thực tế nên hash
        public UserRole Role { get; set; }
    }
}