using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "mission",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<float>(
                name: "AverageStars",
                table: "Nutritionist",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "TotalRatings",
                table: "Nutritionist",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalStars",
                table: "Nutritionist",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageStars",
                table: "Nutritionist");

            migrationBuilder.DropColumn(
                name: "TotalRatings",
                table: "Nutritionist");

            migrationBuilder.DropColumn(
                name: "TotalStars",
                table: "Nutritionist");

            migrationBuilder.AlterColumn<string>(
                name: "mission",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
