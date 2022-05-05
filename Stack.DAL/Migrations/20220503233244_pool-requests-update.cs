using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class poolrequestsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceID",
                table: "PoolRequests");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "PoolRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "PoolRequests",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordID",
                table: "PoolRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequesteeID",
                table: "PoolRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "PoolRequests");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "PoolRequests");

            migrationBuilder.DropColumn(
                name: "RecordID",
                table: "PoolRequests");

            migrationBuilder.DropColumn(
                name: "RequesteeID",
                table: "PoolRequests");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceID",
                table: "PoolRequests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
