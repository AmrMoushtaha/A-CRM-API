using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class teams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(nullable: true),
                    NameAR = table.Column<string>(nullable: true),
                    DescriptionAR = table.Column<string>(nullable: true),
                    DescriptionEN = table.Column<string>(nullable: true),
                    ParentTeamID = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Team_Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    TeamID = table.Column<long>(nullable: false),
                    IsManager = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Users", x => new { x.TeamID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Team_Users_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Team_Users_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Team_Users_UserID",
                table: "Team_Users",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team_Users");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
