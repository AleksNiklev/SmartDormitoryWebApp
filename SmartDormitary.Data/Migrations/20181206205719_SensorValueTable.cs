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
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"f89f40c7-fc6d-4c23-90a8-e5fd3344eec2", "958be9e1-c5bd-45f4-bf05-b35050c793a4"});

            migrationBuilder.DropColumn(
                "Timestamp",
                "Sensors");

            migrationBuilder.DropColumn(
                "Value",
                "Sensors");

            migrationBuilder.AddColumn<int>(
                "SensorDataId",
                "Sensors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                "SensorData",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SensorData", x => x.Id); });

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "fd9823f6-43ec-464e-8ee7-a29212fe779c", "e3a1a418-5641-4474-bcdb-b42079f8fa30", "Administrator",
                    "ADMINISTRATOR"
                });

            migrationBuilder.CreateIndex(
                "IX_Sensors_SensorDataId",
                "Sensors",
                "SensorDataId");

            migrationBuilder.AddForeignKey(
                "FK_Sensors_SensorData_SensorDataId",
                "Sensors",
                "SensorDataId",
                "SensorData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Sensors_SensorData_SensorDataId",
                "Sensors");

            migrationBuilder.DropTable(
                "SensorData");

            migrationBuilder.DropIndex(
                "IX_Sensors_SensorDataId",
                "Sensors");

            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"fd9823f6-43ec-464e-8ee7-a29212fe779c", "e3a1a418-5641-4474-bcdb-b42079f8fa30"});

            migrationBuilder.DropColumn(
                "SensorDataId",
                "Sensors");

            migrationBuilder.AddColumn<DateTime>(
                "Timestamp",
                "Sensors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "Value",
                "Sensors",
                nullable: true);

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "f89f40c7-fc6d-4c23-90a8-e5fd3344eec2", "958be9e1-c5bd-45f4-bf05-b35050c793a4", "Administrator",
                    "ADMINISTRATOR"
                });
        }
    }
}