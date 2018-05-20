using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataLayer.Migrations
{
    public partial class ZoneRectangleChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredZones_MapZones_MapZoneId",
                table: "RegisteredZones");

            migrationBuilder.DropTable(
                name: "MapZones");

            migrationBuilder.DropIndex(
                name: "IX_RegisteredZones_MapZoneId",
                table: "RegisteredZones");

            migrationBuilder.DropColumn(
                name: "MapZoneId",
                table: "RegisteredZones");

            migrationBuilder.CreateTable(
                name: "MapRectangles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    East = table.Column<double>(nullable: false),
                    North = table.Column<double>(nullable: false),
                    South = table.Column<double>(nullable: false),
                    West = table.Column<double>(nullable: false),
                    ZoneId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapRectangles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapRectangles_RegisteredZones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "RegisteredZones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapRectangles_ZoneId",
                table: "MapRectangles",
                column: "ZoneId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapRectangles");

            migrationBuilder.AddColumn<int>(
                name: "MapZoneId",
                table: "RegisteredZones",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MapZones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Altitude = table.Column<string>(nullable: true),
                    FigureType = table.Column<int>(nullable: false),
                    Longitude = table.Column<string>(nullable: true),
                    Size = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapZones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredZones_MapZoneId",
                table: "RegisteredZones",
                column: "MapZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredZones_MapZones_MapZoneId",
                table: "RegisteredZones",
                column: "MapZoneId",
                principalTable: "MapZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
