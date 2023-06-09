using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiroAutoCenter.Data.Migrations
{
    public partial class AddCarStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_UserId",
                table: "Rating",
                newName: "IX_Rating_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "CarStatusId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CarStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClassColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarStatusId",
                table: "Cars",
                column: "CarStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStatus_CarStatusId",
                table: "Cars",
                column: "CarStatusId",
                principalTable: "CarStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStatus_CarStatusId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating");

            migrationBuilder.DropTable(
                name: "CarStatus");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarStatusId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "CarStatusId",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserId",
                table: "Ratings",
                newName: "IX_Ratings_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
