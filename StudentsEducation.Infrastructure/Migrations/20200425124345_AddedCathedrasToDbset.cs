using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class AddedCathedrasToDbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cathedra_CathedraId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cathedra",
                table: "Cathedra");

            migrationBuilder.RenameTable(
                name: "Cathedra",
                newName: "Cathedras");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cathedras",
                table: "Cathedras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cathedras_CathedraId",
                table: "Groups",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cathedras_CathedraId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cathedras",
                table: "Cathedras");

            migrationBuilder.RenameTable(
                name: "Cathedras",
                newName: "Cathedra");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cathedra",
                table: "Cathedra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cathedra_CathedraId",
                table: "Groups",
                column: "CathedraId",
                principalTable: "Cathedra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
