using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class ChandedFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasModified",
                table: "Marks");

            migrationBuilder.AddColumn<bool>(
                name: "WasCorrected",
                table: "Marks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasCorrected",
                table: "Marks");

            migrationBuilder.AddColumn<bool>(
                name: "WasModified",
                table: "Marks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
