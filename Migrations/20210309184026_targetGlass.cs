using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class targetGlass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaretGlasses",
                table: "DietPlanWaterGoals");

            migrationBuilder.AddColumn<int>(
                name: "GlassesUserDrank",
                table: "DietPlanWaterGoals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TargetGlasses",
                table: "DietPlanWaterGoals",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "WaterPercentage",
                table: "DietPlanWaterGoals",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlassesUserDrank",
                table: "DietPlanWaterGoals");

            migrationBuilder.DropColumn(
                name: "TargetGlasses",
                table: "DietPlanWaterGoals");

            migrationBuilder.DropColumn(
                name: "WaterPercentage",
                table: "DietPlanWaterGoals");

            migrationBuilder.AddColumn<float>(
                name: "TaretGlasses",
                table: "DietPlanWaterGoals",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
