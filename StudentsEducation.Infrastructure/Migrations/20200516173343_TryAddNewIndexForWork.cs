using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class TryAddNewIndexForWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Works_SubjectId",
                table: "Works");

            migrationBuilder.CreateIndex(
                name: "IX_Works_SubjectId_WorkNumber",
                table: "Works",
                columns: new[] { "SubjectId", "WorkNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Works_SubjectId_WorkNumber",
                table: "Works");

            migrationBuilder.CreateIndex(
                name: "IX_Works_SubjectId",
                table: "Works",
                column: "SubjectId");
        }
    }
}
