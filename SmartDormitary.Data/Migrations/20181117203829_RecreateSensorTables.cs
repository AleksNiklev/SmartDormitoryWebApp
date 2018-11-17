using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class RecreateSensorTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MeasurementType = table.Column<string>(maxLength: 50, nullable: false),
                    MinRefreshTime = table.Column<int>(nullable: false),
                    MinAcceptableValue = table.Column<int>(nullable: false),
                    MaxAcceptableValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    RefreshTime = table.Column<int>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: true),
                    Latitude = table.Column<string>(maxLength: 100, nullable: false),
                    Longitude = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    SensorTypeId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_SensorTypes_SensorTypeId",
                        column: x => x.SensorTypeId,
                        principalTable: "SensorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorTypeId",
                table: "Sensors",
                column: "SensorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_UserId",
                table: "Sensors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "SensorTypes");
        }
    }
}
