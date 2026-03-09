using System.ComponentModel.DataAnnotations;

namespace qlgiaidau.Models
{
   public class PlayerStat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required String PlayerName { get; set; }

        [Range(0, 100)]
        public int Goals { get; set; }

        [Range(0, 20)]
        public int Assists { get; set; }

        [Range(0, 20)]
        public int TeamId { get; set; }

        public Team? Team { get; set; }

    } 
}
