using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DronZone_API.Migrations
{
    public partial class AddedUpdatedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneValidationRequests_Zones_ZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ZoneValidationRequests_ZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropColumn(
                name: "ZoneId",
                table: "ZoneValidationRequests");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Zones");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "ZoneValidationRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "ZoneSettingsSet",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Zones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "MapRectangles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Drones",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "DronePositionSnapshots",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "AreaFilters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "ZoneValidationRequests");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "ZoneSettingsSet");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Zones");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "MapRectangles");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "DronePositionSnapshots");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "AreaFilters");

            migrationBuilder.AddColumn<string>(
                name: "ZoneId",
                table: "ZoneValidationRequests",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Zones",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ZoneValidationRequests_ZoneId",
                table: "ZoneValidationRequests",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneValidationRequests_Zones_ZoneId",
                table: "ZoneValidationRequests",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
