using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class pool_admin_removed__pool_user_is_admin_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pool_Admins");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Pool_Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Pool_Users");

            migrationBuilder.CreateTable(
                name: "Pool_Admins",
                columns: table => new
                {
                    PoolID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pool_Admins", x => new { x.PoolID, x.UserID });
                    table.ForeignKey(
                        name: "FK_Pool_Admins_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Pool_Admins_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pool_Admins_UserID",
                table: "Pool_Admins",
                column: "UserID");
        }
    }
}
