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
            modelBuilder.Entity<TrainerModel>().HasKey(b => b.email);
            modelBuilder.Entity<TrainingPlanModel>().HasKey(b => b.trainingPlanId);
            //modelBuilder.Entity<ExerciseModel>().HasKey(b => b.exerciseId);
            //modelBuilder.Entity<ExerciseRowModel>().HasKey(b => b.exerciseRowId);
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RegisterModel> RegisterUsers { get; set; }
        public DbSet<TrainerModel> Trainer { get; set; }
        public DbSet<NutritionistModel> Nutritionist { get; set; }
        public DbSet<TrainingPlanModel> TrainingPlan { get; set; }
        //public DbSet<ExerciseModel> ExerciseModel { get; set; }
        //public DbSet<ExerciseRowModel> ExerciseRowModel { get; set; }
    }
}
