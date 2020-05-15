using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class AddedNewFields34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SatisfactorilyValue",
                table: "ControlTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SatisfactorilyValue",
                table: "ControlTypes");
        }
    }
}
