using EF_DietaNoDietaApi.Model;
using Microsoft.EntityFrameworkCore;
 

namespace EF_DietaNoDietaApi.MySql
{
    public class MySqlDbContext : DbContext
    {

        public MySqlDbContext() { 
        
        }
        public MySqlDbContext( DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<UserModel>();
            modelBuilder.Entity<UserModel>().HasKey(b => b.email);
            modelBuilder.Entity<RegisterModel>().HasKey(b => b.email);
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RegisterModel> RegisterUsers { get; set; }
    }
}
