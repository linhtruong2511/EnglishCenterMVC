using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishCenterMVC.Migrations
{
    /// <inheritdoc />
    public partial class Fix2Classes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgresses_AspNetUsers_UserId1",
                table: "LessonProgresses");

            migrationBuilder.DropIndex(
                name: "IX_LessonProgresses_UserId1",
                table: "LessonProgresses");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "LessonProgresses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LessonProgresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgresses_UserId",
                table: "LessonProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgresses_AspNetUsers_UserId",
                table: "LessonProgresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgresses_AspNetUsers_UserId",
                table: "LessonProgresses");

            migrationBuilder.DropIndex(
                name: "IX_LessonProgresses_UserId",
                table: "LessonProgresses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LessonProgresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "LessonProgresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EndDate",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgresses_UserId1",
                table: "LessonProgresses",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgresses_AspNetUsers_UserId1",
                table: "LessonProgresses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
