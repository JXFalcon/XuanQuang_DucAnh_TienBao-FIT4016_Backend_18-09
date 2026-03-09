using System.ComponentModel.DataAnnotations;

namespace qlgiaidau.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đội là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên đội không quá 100 ký tự")]
        public required string Name { get; set; }

        [Url(ErrorMessage = "Logo phải là đường dẫn hợp lệ")]
        public string? LogoUrl { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số trận thắng phải >= 0")]
        public int Wins { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số trận thua phải >= 0")]
        public int Losses { get; set; }
    }
}
