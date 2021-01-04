using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegisterUsers",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    fitnessLevel = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterUsers", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "TrainerModel",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    age = table.Column<string>(nullable: false),
                    experience = table.Column<string>(nullable: false),
                    gender = table.Column<string>(nullable: false),
                    fullName = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerModel", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    email = table.Column<string>(nullable: false),
                    phoneNumber = table.Column<string>(nullable: false),
                    isVeified = table.Column<string>(nullable: true),
                    UserRole = table.Column<string>(nullable: true),
                    fitnessLevel = table.Column<string>(nullable: false),
                    date = table.Column<string>(nullable: true),
                    fullName = table.Column<string>(nullable: true),
                    age = table.Column<string>(nullable: true),
                    height = table.Column<string>(nullable: true),
                    weight = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterUsers");

            migrationBuilder.DropTable(
                name: "TrainerModel");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
