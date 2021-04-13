using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZarzadzanieDomem.Migrations
{
    public partial class Home : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Home");

            migrationBuilder.AddColumn<int>(
                name: "HomeId",
                table: "Home",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Home",
                table: "Home",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    HomeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HomeName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Adress = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.HomeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Home_HomeId",
                table: "Home",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Home_Homes_HomeId",
                table: "Home",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "HomeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Home_Homes_HomeId",
                table: "Home");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Home",
                table: "Home");

            migrationBuilder.DropIndex(
                name: "IX_Home_HomeId",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Home");

            migrationBuilder.RenameTable(
                name: "Home",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");
        }
    }
}
