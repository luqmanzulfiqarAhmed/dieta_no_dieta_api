using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class dietPlanModelUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foodName",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodType",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodUrl",
                table: "foodItems");

            migrationBuilder.AddColumn<string>(
                name: "food_id",
                table: "foodItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "food_name",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "food_type",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "food_url",
                table: "foodItems",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "food_id",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "food_name",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "food_type",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "food_url",
                table: "foodItems");

            migrationBuilder.AddColumn<string>(
                name: "foodName",
                table: "foodItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodType",
                table: "foodItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodUrl",
                table: "foodItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
