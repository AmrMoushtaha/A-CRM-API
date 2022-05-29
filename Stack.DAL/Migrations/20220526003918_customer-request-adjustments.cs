using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class customerrequestadjustments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRequests_CustomerRequestTypes_RequestTypeID",
                table: "CustomerRequests");

            migrationBuilder.AlterColumn<long>(
                name: "RequestTypeID",
                table: "CustomerRequests",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRequests_CustomerRequestTypes_RequestTypeID",
                table: "CustomerRequests",
                column: "RequestTypeID",
                principalTable: "CustomerRequestTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRequests_CustomerRequestTypes_RequestTypeID",
                table: "CustomerRequests");

            migrationBuilder.AlterColumn<long>(
                name: "RequestTypeID",
                table: "CustomerRequests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRequests_CustomerRequestTypes_RequestTypeID",
                table: "CustomerRequests",
                column: "RequestTypeID",
                principalTable: "CustomerRequestTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
