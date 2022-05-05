using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class requestupdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RecordStatusID",
                table: "PoolRequests",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Requestee_PoolID",
                table: "PoolRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordStatusID",
                table: "PoolRequests");

            migrationBuilder.DropColumn(
                name: "Requestee_PoolID",
                table: "PoolRequests");
        }
    }
}
