using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedstagestatusrecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_LeadStatuses_StatusID",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_OpportunityStatuses_StatusID",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_ProspectStatuses_StatusID",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "FirstNameAR",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FirstNameEN",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastNameAR",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastNameEN",
                table: "Customers");

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Prospects",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Opportunities",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Leads",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "FullNameAR",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullNameEN",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_LeadStatuses_StatusID",
                table: "Leads",
                column: "StatusID",
                principalTable: "LeadStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_OpportunityStatuses_StatusID",
                table: "Opportunities",
                column: "StatusID",
                principalTable: "OpportunityStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_ProspectStatuses_StatusID",
                table: "Prospects",
                column: "StatusID",
                principalTable: "ProspectStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_LeadStatuses_StatusID",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Opportunities_OpportunityStatuses_StatusID",
                table: "Opportunities");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_ProspectStatuses_StatusID",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "FullNameAR",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FullNameEN",
                table: "Customers");

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Prospects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Opportunities",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StatusID",
                table: "Leads",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAR",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEN",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameAR",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameEN",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_LeadStatuses_StatusID",
                table: "Leads",
                column: "StatusID",
                principalTable: "LeadStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Opportunities_OpportunityStatuses_StatusID",
                table: "Opportunities",
                column: "StatusID",
                principalTable: "OpportunityStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_ProspectStatuses_StatusID",
                table: "Prospects",
                column: "StatusID",
                principalTable: "ProspectStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
