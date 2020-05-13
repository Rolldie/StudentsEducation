using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class addedIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GradeBookNumber",
                table: "Students",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7);

            migrationBuilder.AddColumn<bool>(
                name: "WasModified",
                table: "Marks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WasModified",
                table: "FinalControls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeBookNumber",
                table: "Students",
                column: "GradeBookNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_GradeBookNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "WasModified",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "WasModified",
                table: "FinalControls");

            migrationBuilder.AlterColumn<string>(
                name: "GradeBookNumber",
                table: "Students",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);
        }
    }
}
