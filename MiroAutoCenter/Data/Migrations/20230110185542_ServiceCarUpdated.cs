using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiroAutoCenter.Data.Migrations
{
    public partial class ServiceCarUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceStatuses_ServiceStatusId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceStatusId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceStatusId",
                table: "Services");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceStatusId",
                table: "ServicesCars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ServicesCars_ServiceStatusId",
                table: "ServicesCars",
                column: "ServiceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesCars_ServiceStatuses_ServiceStatusId",
                table: "ServicesCars",
                column: "ServiceStatusId",
                principalTable: "ServiceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesCars_ServiceStatuses_ServiceStatusId",
                table: "ServicesCars");

            migrationBuilder.DropIndex(
                name: "IX_ServicesCars_ServiceStatusId",
                table: "ServicesCars");

            migrationBuilder.DropColumn(
                name: "ServiceStatusId",
                table: "ServicesCars");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceStatusId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceStatusId",
                table: "Services",
                column: "ServiceStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceStatuses_ServiceStatusId",
                table: "Services",
                column: "ServiceStatusId",
                principalTable: "ServiceStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
