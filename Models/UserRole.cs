using System.ComponentModel.DataAnnotations;

namespace qlgiaidau.Models
{
    public enum UserRole { Admin, Viewer, Coach }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public required string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Viewer;
    } 
}
