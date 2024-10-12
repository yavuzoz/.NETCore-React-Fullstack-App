using Microsoft.EntityFrameworkCore;
using Personal_info_API.Model;
using System.Collections.Generic;

namespace Personal_info_API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Appointments { get; set; }
    }
}
