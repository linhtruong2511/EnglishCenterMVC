using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishCenterMVC.Migrations
{
    /// <inheritdoc />
    public partial class fix_totalSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalSeconds",
                table: "Lessons",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSeconds",
                table: "Lessons");
        }
    }
}
