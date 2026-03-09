using Microsoft.EntityFrameworkCore;
using Web_BongDa_Login.Models;

namespace Web_BongDa_Login.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
