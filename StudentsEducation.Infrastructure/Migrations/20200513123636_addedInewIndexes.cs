﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsEducation.Infrastructure.Migrations
{
    public partial class addedInewIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Marks_WorkId",
                table: "Marks");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_WorkId_StudentId",
                table: "Marks",
                columns: new[] { "WorkId", "StudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name_CourseNumber_StartEducationDate",
                table: "Groups",
                columns: new[] { "Name", "CourseNumber", "StartEducationDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlTypes_ControlName",
                table: "ControlTypes",
                column: "ControlName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Marks_WorkId_StudentId",
                table: "Marks");

            migrationBuilder.DropIndex(
                name: "IX_Groups_Name_CourseNumber_StartEducationDate",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_ControlTypes_ControlName",
                table: "ControlTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_WorkId",
                table: "Marks",
                column: "WorkId");
        }
    }
}
