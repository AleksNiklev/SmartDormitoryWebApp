using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class SensorValueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f89f40c7-fc6d-4c23-90a8-e5fd3344eec2", "958be9e1-c5bd-45f4-bf05-b35050c793a4" });

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Sensors");

            migrationBuilder.AddColumn<int>(
                name: "SensorDataId",
                table: "Sensors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd9823f6-43ec-464e-8ee7-a29212fe779c", "e3a1a418-5641-4474-bcdb-b42079f8fa30", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SensorDataId",
                table: "Sensors",
                column: "SensorDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_SensorData_SensorDataId",
                table: "Sensors",
                column: "SensorDataId",
                principalTable: "SensorData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_SensorData_SensorDataId",
                table: "Sensors");

            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_SensorDataId",
                table: "Sensors");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fd9823f6-43ec-464e-8ee7-a29212fe779c", "e3a1a418-5641-4474-bcdb-b42079f8fa30" });

            migrationBuilder.DropColumn(
                name: "SensorDataId",
                table: "Sensors");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Sensors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Sensors",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f89f40c7-fc6d-4c23-90a8-e5fd3344eec2", "958be9e1-c5bd-45f4-bf05-b35050c793a4", "Administrator", "ADMINISTRATOR" });
        }
    }
}
