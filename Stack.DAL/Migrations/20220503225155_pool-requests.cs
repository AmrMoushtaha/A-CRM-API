using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class poolrequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoolRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolID = table.Column<long>(nullable: false),
                    ReferenceID = table.Column<string>(nullable: true),
                    RequestType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PoolRequests_Pools_PoolID",
                        column: x => x.PoolID,
                        principalTable: "Pools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolRequests_PoolID",
                table: "PoolRequests",
                column: "PoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolRequests");
        }
    }
}
