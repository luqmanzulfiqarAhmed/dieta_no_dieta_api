using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class YourMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "isVeified",
                table: "Users",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isVeified",
                table: "Users",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
