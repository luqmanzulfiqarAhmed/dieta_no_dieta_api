using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class secondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerModel");
        }
    }
}
