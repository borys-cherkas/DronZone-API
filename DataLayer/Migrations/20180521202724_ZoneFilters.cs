using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class ZoneFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ZoneSettingsId",
                table: "DroneFilters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DroneFilters_ZoneSettingsId",
                table: "DroneFilters",
                column: "ZoneSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DroneFilters_ZoneSettingsSet_ZoneSettingsId",
                table: "DroneFilters",
                column: "ZoneSettingsId",
                principalTable: "ZoneSettingsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DroneFilters_ZoneSettingsSet_ZoneSettingsId",
                table: "DroneFilters");

            migrationBuilder.DropIndex(
                name: "IX_DroneFilters_ZoneSettingsId",
                table: "DroneFilters");

            migrationBuilder.DropColumn(
                name: "ZoneSettingsId",
                table: "DroneFilters");
        }
    }
}
