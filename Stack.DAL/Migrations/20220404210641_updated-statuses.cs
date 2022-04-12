using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedstatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProspectID",
                table: "ProspectStatuses");

            migrationBuilder.DropColumn(
                name: "OpportunityID",
                table: "OpportunityStatuses");

            migrationBuilder.DropColumn(
                name: "LeadID",
                table: "LeadStatuses");

            migrationBuilder.AlterColumn<long>(
                name: "NewStatus",
                table: "SubmissionDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CurrentStatus",
                table: "SubmissionDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NewStatus",
                table: "SubmissionDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "CurrentStatus",
                table: "SubmissionDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "ProspectID",
                table: "ProspectStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OpportunityID",
                table: "OpportunityStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LeadID",
                table: "LeadStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
