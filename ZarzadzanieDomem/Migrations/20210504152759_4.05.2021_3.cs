using Microsoft.EntityFrameworkCore.Migrations;

namespace ZarzadzanieDomem.Migrations
{
    public partial class _4052021_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ActivationToken",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationToken",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
