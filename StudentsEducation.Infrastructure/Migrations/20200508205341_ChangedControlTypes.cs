using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class ChangedControlTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueDifference",
                table: "ControlTypes");

            migrationBuilder.AddColumn<int>(
                name: "HighValue",
                table: "ControlTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LowValue",
                table: "ControlTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighValue",
                table: "ControlTypes");

            migrationBuilder.DropColumn(
                name: "LowValue",
                table: "ControlTypes");

            migrationBuilder.AddColumn<string>(
                name: "ValueDifference",
                table: "ControlTypes",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");
        }
    }
}
