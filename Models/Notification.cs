using System.ComponentModel.DataAnnotations;

namespace qlgiaidau.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tiêu đề không quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;   // khởi tạo mặc định

        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        [StringLength(1000, ErrorMessage = "Nội dung không quá 1000 ký tự")]
        public string Message { get; set; } = string.Empty; // khởi tạo mặc định

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}