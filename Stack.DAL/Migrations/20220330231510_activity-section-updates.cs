using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class activitysectionupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySections_Activities_ActivityID",
                table: "ActivitySections");

            migrationBuilder.AlterColumn<long>(
                name: "SectionID",
                table: "ActivitySections",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ActivityID",
                table: "ActivitySections",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySections_Activities_ActivityID",
                table: "ActivitySections",
                column: "ActivityID",
                principalTable: "Activities",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySections_Activities_ActivityID",
                table: "ActivitySections");

            migrationBuilder.AlterColumn<long>(
                name: "SectionID",
                table: "ActivitySections",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ActivityID",
                table: "ActivitySections",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySections_Activities_ActivityID",
                table: "ActivitySections",
                column: "ActivityID",
                principalTable: "Activities",
                principalColumn: "ID");
        }
    }
}
