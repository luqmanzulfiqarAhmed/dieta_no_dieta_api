using DietaNoDietaApi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DietaNoDietaApi.MySql
{
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
