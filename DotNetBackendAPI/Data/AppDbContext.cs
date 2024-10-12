using Microsoft.EntityFrameworkCore;
using Personal_info_API.Data.Model;
using System.Collections.Generic;

namespace Personal_info_API.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-VQGH9P1\\SQLEXPRESS;initial Catalog=MyApiDb;integrated Security=true;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
    }
}
