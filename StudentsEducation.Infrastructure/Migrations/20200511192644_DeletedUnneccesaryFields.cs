using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class DeletedUnneccesaryFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CourseNumber",
                table: "Subjects");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Works",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Subjects",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Works",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Subjects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseNumber",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_ControlTypes_ControlTypeId",
                table: "Works",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
