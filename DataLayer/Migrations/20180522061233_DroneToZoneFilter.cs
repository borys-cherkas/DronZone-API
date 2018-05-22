using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class DroneToZoneFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DroneFilters");

            migrationBuilder.CreateTable(
                name: "AreaFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    DroneType = table.Column<int>(nullable: false),
                    MaxAvailableWeigth = table.Column<double>(nullable: false),
                    MaxDroneSpeed = table.Column<double>(nullable: false),
                    MaxDroneWeigth = table.Column<double>(nullable: false),
                    ZoneSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaFilters_ZoneSettingsSet_ZoneSettingsId",
                        column: x => x.ZoneSettingsId,
                        principalTable: "ZoneSettingsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaFilters_ZoneSettingsId",
                table: "AreaFilters",
                column: "ZoneSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaFilters");

            migrationBuilder.CreateTable(
                name: "DroneFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    DroneType = table.Column<int>(nullable: false),
                    MaxAvailableWeigth = table.Column<double>(nullable: false),
                    MaxDroneSpeed = table.Column<double>(nullable: false),
                    MaxDroneWeigth = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ZoneSettingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DroneFilters_ZoneSettingsSet_ZoneSettingsId",
                        column: x => x.ZoneSettingsId,
                        principalTable: "ZoneSettingsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroneFilters_ZoneSettingsId",
                table: "DroneFilters",
                column: "ZoneSettingsId");
        }
    }
}
