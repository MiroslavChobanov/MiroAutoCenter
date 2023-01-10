using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiroAutoCenter.Data.Migrations
{
    public partial class ServiceUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_UserId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Services");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
