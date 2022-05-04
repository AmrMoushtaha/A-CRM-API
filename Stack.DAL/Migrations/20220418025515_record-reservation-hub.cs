using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class recordreservationhub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectionIDs",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 256, nullable: false),
                    PoolID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionIDs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConnectionIDs_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionIDs_UserID",
                table: "ConnectionIDs",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionIDs");
        }
    }
}
