using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_DietaNoDietaApi.Migrations
{
    public partial class goals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietPlanGoals",
                columns: table => new
                {
                    GoalId = table.Column<Guid>(nullable: false),
                    NutrtionistEmail = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    StartingDate = table.Column<string>(nullable: false),
                    EndingDate = table.Column<string>(nullable: false),
                    ExpectedResult = table.Column<string>(nullable: false),
                    GoalType = table.Column<string>(nullable: false),
                    isCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietPlanGoals", x => x.GoalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietPlanGoals");
        }
    }
}
