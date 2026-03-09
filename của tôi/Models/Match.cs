using System.ComponentModel.DataAnnotations;

namespace qlgiaidau.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeamAId { get; set; }   // Khóa ngoại tới Team

        [Required]
        public int TeamBId { get; set; }   // Khóa ngoại tới Team

        [Required(ErrorMessage = "Ngày giờ thi đấu là bắt buộc")]
        public DateTime MatchDate { get; set; }

        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        // Navigation properties
        public Team? TeamA { get; set; }
        public Team? TeamB { get; set; }
    }
}