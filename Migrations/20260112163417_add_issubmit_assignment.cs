using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishCenterMVC.Migrations
{
    /// <inheritdoc />
    public partial class add_issubmit_assignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmit",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmit",
                table: "Assignments");
        }
    }
}
