using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class updateWeightGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedResult",
                table: "DietPlanGoals");

            migrationBuilder.AddColumn<string>(
                name: "CurrentWeight",
                table: "DietPlanGoals",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TargetWeight",
                table: "DietPlanGoals",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentWeight",
                table: "DietPlanGoals");

            migrationBuilder.DropColumn(
                name: "TargetWeight",
                table: "DietPlanGoals");

            migrationBuilder.AddColumn<string>(
                name: "ExpectedResult",
                table: "DietPlanGoals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
