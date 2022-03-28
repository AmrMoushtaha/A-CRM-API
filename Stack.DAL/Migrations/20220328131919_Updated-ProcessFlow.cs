using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class UpdatedProcessFlow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlows_Customers_CustomerID",
                table: "ProcessFlows");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerID",
                table: "ProcessFlows",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlows_Customers_CustomerID",
                table: "ProcessFlows",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessFlows_Customers_CustomerID",
                table: "ProcessFlows");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerID",
                table: "ProcessFlows",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessFlows_Customers_CustomerID",
                table: "ProcessFlows",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
