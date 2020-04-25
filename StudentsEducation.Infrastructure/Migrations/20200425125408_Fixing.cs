using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class Fixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cathedras_CathedraId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CathedraId",
                table: "Groups",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cathedras_CathedraId",
                table: "Groups",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cathedras_CathedraId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "CathedraId",
                table: "Groups",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cathedras_CathedraId",
                table: "Groups",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
