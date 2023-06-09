using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiroAutoCenter.Data.Migrations
{
    public partial class UpdateCarStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStatus_CarStatusId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarStatus",
                table: "CarStatus");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "CarStatus",
                newName: "CarStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserId",
                table: "Ratings",
                newName: "IX_Ratings_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarStatuses",
                table: "CarStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarStatuses_CarStatusId",
                table: "Cars",
                column: "CarStatusId",
                principalTable: "CarStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarStatuses_CarStatusId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarStatuses",
                table: "CarStatuses");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameTable(
                name: "CarStatuses",
                newName: "CarStatus");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_UserId",
                table: "Rating",
                newName: "IX_Rating_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarStatus",
                table: "CarStatus",
                column: "Id");

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
    }
}
