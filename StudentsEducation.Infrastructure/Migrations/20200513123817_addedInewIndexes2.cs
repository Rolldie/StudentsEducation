using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class addedInewIndexes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Marks_WorkId_StudentId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_ControlTypes_ControlName",
                table: "ControlTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_WorkId_StudentId",
                table: "Marks",
                columns: new[] { "WorkId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlTypes_ControlName",
                table: "ControlTypes",
                column: "ControlName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Marks_WorkId_StudentId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_ControlTypes_ControlName",
                table: "ControlTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_WorkId_StudentId",
                table: "Marks",
                columns: new[] { "WorkId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_ControlTypes_ControlName",
                table: "ControlTypes",
                column: "ControlName");
        }
    }
}
