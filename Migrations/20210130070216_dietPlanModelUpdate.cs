using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class dietPlanModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sleepHours",
                table: "DietPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sleepQuality",
                table: "DietPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "thieSize",
                table: "DietPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "water",
                table: "DietPlans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "weight",
                table: "DietPlans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "water",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "DietPlans");
        }
    }
}
