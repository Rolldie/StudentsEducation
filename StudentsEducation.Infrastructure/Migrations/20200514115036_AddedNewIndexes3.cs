using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class AddedNewIndexes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinalControls_StudentId",
                table: "FinalControls");

            migrationBuilder.CreateIndex(
                name: "IX_FinalControls_StudentId_SubjectId",
                table: "FinalControls",
                columns: new[] { "StudentId", "SubjectId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinalControls_StudentId_SubjectId",
                table: "FinalControls");

            migrationBuilder.CreateIndex(
                name: "IX_FinalControls_StudentId",
                table: "FinalControls",
                column: "StudentId");
        }
    }
}
