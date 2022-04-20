using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class poolconnectionidsupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionIDs_AspNetUsers_UserID",
                table: "ConnectionIDs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionIDs",
                table: "ConnectionIDs");

            migrationBuilder.RenameTable(
                name: "ConnectionIDs",
                newName: "PoolConnectionIDs");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectionIDs_UserID",
                table: "PoolConnectionIDs",
                newName: "IX_PoolConnectionIDs_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PoolConnectionIDs",
                table: "PoolConnectionIDs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PoolConnectionIDs_AspNetUsers_UserID",
                table: "PoolConnectionIDs",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoolConnectionIDs_AspNetUsers_UserID",
                table: "PoolConnectionIDs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PoolConnectionIDs",
                table: "PoolConnectionIDs");

            migrationBuilder.RenameTable(
                name: "PoolConnectionIDs",
                newName: "ConnectionIDs");

            migrationBuilder.RenameIndex(
                name: "IX_PoolConnectionIDs_UserID",
                table: "ConnectionIDs",
                newName: "IX_ConnectionIDs_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionIDs",
                table: "ConnectionIDs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionIDs_AspNetUsers_UserID",
                table: "ConnectionIDs",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
