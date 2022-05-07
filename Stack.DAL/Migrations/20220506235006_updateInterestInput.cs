using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateInterestInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LInterestInputs_Inputs_InputID",
                table: "LInterestInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_LInterestInputs_LInterests_LInterestID",
                table: "LInterestInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_LInterests_LInterests_ParentLInterestID",
                table: "LInterests");

            migrationBuilder.DropIndex(
                name: "IX_LInterests_ParentLInterestID",
                table: "LInterests");

            migrationBuilder.DropIndex(
                name: "IX_LInterestInputs_InputID",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "AttributeID",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "InputField",
                table: "LInterestInputs");

            migrationBuilder.DropColumn(
                name: "InputType",
                table: "LInterestInputs");

            migrationBuilder.AlterColumn<long>(
                name: "LInterestID",
                table: "LInterestInputs",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SelectedAttributeID",
                table: "LInterestInputs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerComments",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: true),
                    CustomerID = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerComments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerComments_CustomerID",
                table: "CustomerComments",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_LInterestInputs_LInterests_LInterestID",
                table: "LInterestInputs",
                column: "LInterestID",
                principalTable: "LInterests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LInterestInputs_LInterests_LInterestID",
                table: "LInterestInputs");

            migrationBuilder.DropTable(
                name: "CustomerComments");

            migrationBuilder.DropColumn(
                name: "SelectedAttributeID",
                table: "LInterestInputs");

            migrationBuilder.AlterColumn<long>(
                name: "LInterestID",
                table: "LInterestInputs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "AttributeID",
                table: "LInterestInputs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "InputField",
                table: "LInterestInputs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "LInterestInputs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LInterests_ParentLInterestID",
                table: "LInterests",
                column: "ParentLInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_LInterestInputs_InputID",
                table: "LInterestInputs",
                column: "InputID");

            migrationBuilder.AddForeignKey(
                name: "FK_LInterestInputs_Inputs_InputID",
                table: "LInterestInputs",
                column: "InputID",
                principalTable: "Inputs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LInterestInputs_LInterests_LInterestID",
                table: "LInterestInputs",
                column: "LInterestID",
                principalTable: "LInterests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LInterests_LInterests_ParentLInterestID",
                table: "LInterests",
                column: "ParentLInterestID",
                principalTable: "LInterests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
