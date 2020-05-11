using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Works",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Works",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
