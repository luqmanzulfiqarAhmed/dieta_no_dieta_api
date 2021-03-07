using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class watergoals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietPlanWaterGoals",
                columns: table => new
                {
                    GoalId = table.Column<Guid>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    NeutrtionistEmail = table.Column<string>(nullable: false),
                    WaterInLtrs = table.Column<string>(nullable: false),
                    isCompleted = table.Column<string>(nullable: true),
                    TaretGlasses = table.Column<float>(nullable: false),
                    PerGlassValue = table.Column<float>(nullable: false),
                    AcheivedValue = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlanWaterGoals", x => x.GoalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietPlanWaterGoals");
        }
    }
}
