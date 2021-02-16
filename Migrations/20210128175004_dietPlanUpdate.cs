using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class dietPlanUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foodTime",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "sleepHours",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "sleepQuality",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "thieSize",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "trainerEmail",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "vaste",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "water",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "DietPlans");

            migrationBuilder.AddColumn<string>(
                name: "foodUrl",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodTime",
                table: "DietPlans",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "neutrtionistEmail",
                table: "DietPlans",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foodUrl",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodTime",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "neutrtionistEmail",
                table: "DietPlans");

            migrationBuilder.AddColumn<string>(
                name: "foodTime",
                table: "foodItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sleepHours",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sleepQuality",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "thieSize",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trainerEmail",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "vaste",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "water",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "weight",
                table: "DietPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
