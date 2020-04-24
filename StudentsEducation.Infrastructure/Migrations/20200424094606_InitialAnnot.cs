using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class InitialAnnot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CathedraId",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cathedra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    MainPhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    SecondPhoneNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedra", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CathedraId",
                table: "Groups",
                column: "CathedraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Cathedra_CathedraId",
                table: "Groups",
                column: "CathedraId",
                principalTable: "Cathedra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Cathedra_CathedraId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Cathedra");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CathedraId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CathedraId",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}
