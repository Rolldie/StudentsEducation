using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class fixedControlType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_WorkControlTypes_WorkControlTypeId",
                table: "Works");

            migrationBuilder.DropTable(
                name: "WorkControlTypes");

            migrationBuilder.DropIndex(
                name: "IX_Works_WorkControlTypeId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "WorkControlTypeId",
                table: "Works");

            migrationBuilder.AddColumn<int>(
                name: "ControlTypeId",
                table: "Works",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_ControlTypeId",
                table: "Works",
                column: "ControlTypeId");

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

            migrationBuilder.DropIndex(
                name: "IX_Works_ControlTypeId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "ControlTypeId",
                table: "Works");

            migrationBuilder.AddColumn<int>(
                name: "WorkControlTypeId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkControlTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControlName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ValueDifference = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkControlTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkControlTypeId",
                table: "Works",
                column: "WorkControlTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_WorkControlTypes_WorkControlTypeId",
                table: "Works",
                column: "WorkControlTypeId",
                principalTable: "WorkControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
