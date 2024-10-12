using Microsoft.EntityFrameworkCore;
using Personal_info_API.Data.Model;using Personal_info_API.Data;

namespace Personal_info_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
