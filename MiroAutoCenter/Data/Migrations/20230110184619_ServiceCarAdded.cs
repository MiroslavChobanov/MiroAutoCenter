using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiroAutoCenter.Data.Migrations
{
    public partial class ServiceCarAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Cars_CarId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CarId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "ServicesCars",
                columns: table => new
                {
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesCars", x => new { x.CarId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServicesCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesCars_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicesCars_ServiceId",
                table: "ServicesCars",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicesCars");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Services_CarId",
                table: "Services",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Cars_CarId",
                table: "Services",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
