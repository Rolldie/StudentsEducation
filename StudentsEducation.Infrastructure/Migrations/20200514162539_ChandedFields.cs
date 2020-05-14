using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class ChandedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skips_Students_StudentId",
                table: "Skips");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Skips",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Skips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Skips_Students_StudentId",
                table: "Skips",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skips_Students_StudentId",
                table: "Skips");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Skips");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Skips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Skips_Students_StudentId",
                table: "Skips",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
