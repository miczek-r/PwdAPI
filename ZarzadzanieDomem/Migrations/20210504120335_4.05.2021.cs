using Microsoft.EntityFrameworkCore.Migrations;

namespace ZarzadzanieDomem.Migrations
{
    public partial class _4052021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExpenseLimit",
                table: "Users",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenseLimit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Users");
        }
    }
}
