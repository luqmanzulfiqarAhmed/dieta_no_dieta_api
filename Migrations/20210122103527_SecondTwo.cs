using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class SecondTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "foodDescription");

            migrationBuilder.AddColumn<string>(
                name: "foodCalories",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodCarbs",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodFat",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodProtein",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "foodQuantity",
                table: "foodItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "missionName",
                table: "foodItems",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "foodCalories",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodCarbs",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodFat",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodProtein",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "foodQuantity",
                table: "foodItems");

            migrationBuilder.DropColumn(
                name: "missionName",
                table: "foodItems");

            migrationBuilder.CreateTable(
                name: "foodDescription",
                columns: table => new
                {
                    foodDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FoodItemsModelfoodItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    foodCalories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foodCarbs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foodFat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foodProtein = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foodQuantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    missionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_foodDescription_FoodItemsModelfoodItemId",
                table: "foodDescription",
                column: "FoodItemsModelfoodItemId");
        }
    }
}
