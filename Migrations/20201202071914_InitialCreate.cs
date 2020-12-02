using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    email = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<string>(nullable: false),
                    isVeified = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: false),
                    fitnessLevel = table.Column<string>(nullable: false)

                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
