﻿// <auto-generated />
using System;
using EF_DietaNoDietaApi.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EF_DietaNoDietaApi.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    partial class MySqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.DietPlanModel", b =>
                {
                    b.Property<Guid>("dietPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dietPlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("isWishlist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("planType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sleepHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sleepQuality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("thieSize")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trainerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("vaste")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("water")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("weight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("dietPlanId");

                    b.ToTable("DietPlans");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.ExerciseModel", b =>
                {
                    b.Property<Guid>("exerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TrainingPlanModeltrainingPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("exerciseType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("exerciseId");

                    b.HasIndex("TrainingPlanModeltrainingPlanId");

                    b.ToTable("ExerciseModel");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.ExerciseRowModel", b =>
                {
                    b.Property<Guid>("exerciseRowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ExerciseModelexerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("exerciseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("raps")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("exerciseRowId");

                    b.HasIndex("ExerciseModelexerciseId");

                    b.ToTable("ExerciseRowModel");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.FoodDescriptionModel", b =>
                {
                    b.Property<Guid>("foodDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FoodItemsModelfoodItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("foodCalories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("foodCarbs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("foodFat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("foodProtein")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("foodQuantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("missionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("foodDescriptionId");

                    b.HasIndex("FoodItemsModelfoodItemId");

                    b.ToTable("foodDescription");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.FoodItemsModel", b =>
                {
                    b.Property<Guid>("foodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DietPlanModeldietPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("foodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("foodTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("foodItemId");

                    b.HasIndex("DietPlanModeldietPlanId");

                    b.ToTable("foodItems");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.NutritionistModel", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("Nutritionist");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.RegisterModel", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fitnessLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("RegisterUsers");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.TrainerModel", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("Trainer");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.TrainingPlanModel", b =>
                {
                    b.Property<Guid>("trainingPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("numOfHrs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trainerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("trainingPlanId");

                    b.ToTable("TrainingPlan");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.UserModel", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("age")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currentBiseps")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currentHips")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currentThai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currentVaste")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currentWeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fitnessLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("isVeified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("neutritionistEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("objectiveBiseps")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("objectiveHips")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("objectiveThai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("objectiveVaste")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("objectiveWeight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("trainerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.ExerciseModel", b =>
                {
                    b.HasOne("EF_DietaNoDietaApi.Model.TrainingPlanModel", null)
                        .WithMany("exercises")
                        .HasForeignKey("TrainingPlanModeltrainingPlanId");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.ExerciseRowModel", b =>
                {
                    b.HasOne("EF_DietaNoDietaApi.Model.ExerciseModel", null)
                        .WithMany("exerciseRows")
                        .HasForeignKey("ExerciseModelexerciseId");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.FoodDescriptionModel", b =>
                {
                    b.HasOne("EF_DietaNoDietaApi.Model.FoodItemsModel", null)
                        .WithMany("foodDescriptionModels")
                        .HasForeignKey("FoodItemsModelfoodItemId");
                });

            modelBuilder.Entity("EF_DietaNoDietaApi.Model.FoodItemsModel", b =>
                {
                    b.HasOne("EF_DietaNoDietaApi.Model.DietPlanModel", null)
                        .WithMany("foodItemsModels")
                        .HasForeignKey("DietPlanModeldietPlanId");
                });
#pragma warning restore 612, 618
        }
    }
}
