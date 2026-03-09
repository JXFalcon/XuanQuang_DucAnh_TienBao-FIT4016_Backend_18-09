using System.ComponentModel.DataAnnotations;

namespace Web_BongDa_Login.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "User"; // mặc định là User
    }
}
