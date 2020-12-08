using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false),
                    isVeified = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: false),
                    fitnessLevel = table.Column<string>(nullable: false),
                    date = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
