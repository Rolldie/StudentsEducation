using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class ChangedIdexes111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubjectId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherId_SubjectId_GroupId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectId_GroupId",
                table: "Schedules",
                columns: new[] { "SubjectId", "GroupId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SubjectId_GroupId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectId",
                table: "Schedules",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherId_SubjectId_GroupId",
                table: "Schedules",
                columns: new[] { "TeacherId", "SubjectId", "GroupId" },
                unique: true);
        }
    }
}
