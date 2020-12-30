using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietPlans",
                columns: table => new
                {
                    dietPlanId = table.Column<Guid>(nullable: false),
                    userEmail = table.Column<string>(nullable: true),
                    trainerEmail = table.Column<string>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    weight = table.Column<string>(nullable: false),
                    water = table.Column<string>(nullable: false),
                    thieSize = table.Column<string>(nullable: false),
                    vaste = table.Column<string>(nullable: false),
                    sleepHours = table.Column<string>(nullable: false),
                    sleepQuality = table.Column<string>(nullable: false),
                    dietPlanName = table.Column<string>(nullable: false),
                    planType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlans", x => x.dietPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Nutritionist",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    age = table.Column<string>(nullable: false),
                    experience = table.Column<string>(nullable: false),
                    gender = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutritionist", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "RegisterUsers",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    fitnessLevel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterUsers", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    age = table.Column<string>(nullable: false),
                    experience = table.Column<string>(nullable: false),
                    gender = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlan",
                columns: table => new
                {
                    trainingPlanId = table.Column<Guid>(nullable: false),
                    userEmail = table.Column<string>(nullable: false),
                    trainerEmail = table.Column<string>(nullable: false),
                    duration = table.Column<string>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    numOfHrs = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlan", x => x.trainingPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false),
                    isVeified = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    fitnessLevel = table.Column<string>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    fullName = table.Column<string>(nullable: true),
                    age = table.Column<string>(nullable: true),
                    height = table.Column<string>(nullable: true),
                    currentWeight = table.Column<string>(nullable: true),
                    objectiveWeight = table.Column<string>(nullable: true),
                    currentVaste = table.Column<string>(nullable: true),
                    objectiveVaste = table.Column<string>(nullable: true),
                    currentBiseps = table.Column<string>(nullable: true),
                    objectiveBiseps = table.Column<string>(nullable: true),
                    currentHips = table.Column<string>(nullable: true),
                    objectiveHips = table.Column<string>(nullable: true),
                    currentThai = table.Column<string>(nullable: true),
                    objectiveThai = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    imc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "foodItems",
                columns: table => new
                {
                    foodItemId = table.Column<Guid>(nullable: false),
                    foodName = table.Column<string>(nullable: false),
                    foodTime = table.Column<string>(nullable: false),
                    DietPlanModeldietPlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodItems", x => x.foodItemId);
                    table.ForeignKey(
                        name: "FK_foodItems_DietPlans_DietPlanModeldietPlanId",
                        column: x => x.DietPlanModeldietPlanId,
                        principalTable: "DietPlans",
                        principalColumn: "dietPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseModel",
                columns: table => new
                {
                    exerciseId = table.Column<Guid>(nullable: false),
                    exerciseType = table.Column<string>(nullable: false),
                    TrainingPlanModeltrainingPlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseModel", x => x.exerciseId);
                    table.ForeignKey(
                        name: "FK_ExerciseModel_TrainingPlan_TrainingPlanModeltrainingPlanId",
                        column: x => x.TrainingPlanModeltrainingPlanId,
                        principalTable: "TrainingPlan",
                        principalColumn: "trainingPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "foodDescription",
                columns: table => new
                {
                    foodDescriptionId = table.Column<Guid>(nullable: false),
                    missionName = table.Column<string>(nullable: false),
                    foodQuantity = table.Column<string>(nullable: false),
                    foodCalories = table.Column<string>(nullable: false),
                    foodFat = table.Column<string>(nullable: false),
                    foodCarbs = table.Column<string>(nullable: false),
                    foodProtein = table.Column<string>(nullable: false),
                    FoodItemsModelfoodItemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodDescription", x => x.foodDescriptionId);
                    table.ForeignKey(
                        name: "FK_foodDescription_foodItems_FoodItemsModelfoodItemId",
                        column: x => x.FoodItemsModelfoodItemId,
                        principalTable: "foodItems",
                        principalColumn: "foodItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseRowModel",
                columns: table => new
                {
                    exerciseRowId = table.Column<Guid>(nullable: false),
                    exerciseName = table.Column<string>(nullable: false),
                    raps = table.Column<string>(nullable: false),
                    time = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    ExerciseModelexerciseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseRowModel", x => x.exerciseRowId);
                    table.ForeignKey(
                        name: "FK_ExerciseRowModel_ExerciseModel_ExerciseModelexerciseId",
                        column: x => x.ExerciseModelexerciseId,
                        principalTable: "ExerciseModel",
                        principalColumn: "exerciseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseModel_TrainingPlanModeltrainingPlanId",
                table: "ExerciseModel",
                column: "TrainingPlanModeltrainingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRowModel_ExerciseModelexerciseId",
                table: "ExerciseRowModel",
                column: "ExerciseModelexerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_foodDescription_FoodItemsModelfoodItemId",
                table: "foodDescription",
                column: "FoodItemsModelfoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_foodItems_DietPlanModeldietPlanId",
                table: "foodItems",
                column: "DietPlanModeldietPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseRowModel");

            migrationBuilder.DropTable(
                name: "foodDescription");

            migrationBuilder.DropTable(
                name: "Nutritionist");

            migrationBuilder.DropTable(
                name: "RegisterUsers");

            migrationBuilder.DropTable(
                name: "Trainer");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ExerciseModel");

            migrationBuilder.DropTable(
                name: "foodItems");

            migrationBuilder.DropTable(
                name: "TrainingPlan");

            migrationBuilder.DropTable(
                name: "DietPlans");
        }
    }
}
