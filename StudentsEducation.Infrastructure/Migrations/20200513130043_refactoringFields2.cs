using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class refactoringFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ControlTypeId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Marks");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Subjects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "Marks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateToPass",
                table: "Marks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ControlTypeId_Name",
                table: "Subjects",
                columns: new[] { "ControlTypeId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherId_SubjectId_GroupId",
                table: "Schedules",
                columns: new[] { "TeacherId", "SubjectId", "GroupId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ControlTypeId_Name",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherId_SubjectId_GroupId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "Marks");

            migrationBuilder.DropColumn(
                name: "DateToPass",
                table: "Marks");

            migrationBuilder.AlterColumn<int>(
                name: "ControlTypeId",
                table: "Subjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Marks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ControlTypeId",
                table: "Subjects",
                column: "ControlTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_ControlTypes_ControlTypeId",
                table: "Subjects",
                column: "ControlTypeId",
                principalTable: "ControlTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
