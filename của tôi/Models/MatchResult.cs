using System.ComponentModel.DataAnnotations;

namespace qlgiaidau.Models
{
    public class MatchResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Range(0, 50, ErrorMessage = "Số bàn thắng phải từ 0 đến 50")]
        public int TeamAScore { get; set; }

        [Range(0, 50, ErrorMessage = "Số bàn thắng phải từ 0 đến 50")]
        public int TeamBScore { get; set; }

        public Match? Match { get; set; }
    }
}
