using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class SeedSensorTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SensorTypes",
                newName: "Tag");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SensorTypes",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "SensorTypes",
                columns: new[] { "Id", "Description", "MaxAcceptableValue", "MeasurementType", "MinAcceptableValue", "MinRefreshTime", "Tag" },
                values: new object[,]
                {
                    { new Guid("f1796a28-642e-401f-8129-fd7465417061"), "This sensor will return values between 15 and 28", 28, "°C", 15, 40, "TemperatureSensor1" },
                    { new Guid("81a2e1b1-ea5d-4356-8266-b6b42471653e"), "This sensor will return values between 6 and 18", 18, "°C", 6, 30, "TemperatureSensor2" },
                    { new Guid("92f7dc9a-f2fe-4b60-82f5-400e42f099b4"), "This sensor will return values between 19 and 23", 23, "°C", 19, 70, "TemperatureSensor3" },
                    { new Guid("216fc1e7-1496-4532-b9ee-29565b865ad6"), "This sensor will return values between 0 and 60", 60, "%", 0, 40, "HumiditySensor1" },
                    { new Guid("61ff0614-64fd-4842-9a05-0b1541d2cc61"), "This sensor will return values between 10 and 90", 90, "%", 10, 50, "HumiditySensor2" },
                    { new Guid("08503c1c-963f-4106-9088-82fa67d34f9d"), "This sensor will return values between 1000 and 5000", 5000, "W", 1000, 70, "ElectricPowerConsumtionSensor1" },
                    { new Guid("1f0ef0ff-396b-40cb-ac3d-749196dee187"), "This sensor will return values between 500 and 3500", 3500, "W", 500, 105, "ElectricPowerConsumtionSensor2" },
                    { new Guid("4008e030-fd3a-4f8c-a8ca-4f7609ecdb1e"), "This sensor will return true or false value", 1, "(true/false)", 0, 50, "OccupancySensor1" },
                    { new Guid("7a3b1db5-959d-46ce-82b6-517773327427"), "This sensor will return true or false value", 1, "(true/false)", 0, 80, "OccupancySensor2" },
                    { new Guid("a3b8a078-0409-4365-ace6-6f8b5b93d592"), "This sensor will return true or false value", 1, "(true/false)", 0, 90, "DoorSensor1" },
                    { new Guid("ec3c4770-5d57-4d81-9c83-a02140b883a1"), "This sensor will return true or false value", 1, "(true/false)", 0, 50, "DoorSensor2" },
                    { new Guid("d5d37a46-8ab5-41ec-b7d5-d28c2fd68d3d"), "This sensor will return values between 20 and 70", 70, "dB", 20, 40, "NoiseSensor1" },
                    { new Guid("65d98ff7-b524-49de-8d13-f85753d98858"), "This sensor will return values between 40 and 90", 90, "dB", 40, 85, "NoiseSensor2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("08503c1c-963f-4106-9088-82fa67d34f9d"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("1f0ef0ff-396b-40cb-ac3d-749196dee187"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("216fc1e7-1496-4532-b9ee-29565b865ad6"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("4008e030-fd3a-4f8c-a8ca-4f7609ecdb1e"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("61ff0614-64fd-4842-9a05-0b1541d2cc61"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("65d98ff7-b524-49de-8d13-f85753d98858"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("7a3b1db5-959d-46ce-82b6-517773327427"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("81a2e1b1-ea5d-4356-8266-b6b42471653e"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("92f7dc9a-f2fe-4b60-82f5-400e42f099b4"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("a3b8a078-0409-4365-ace6-6f8b5b93d592"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("d5d37a46-8ab5-41ec-b7d5-d28c2fd68d3d"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("ec3c4770-5d57-4d81-9c83-a02140b883a1"));

            migrationBuilder.DeleteData(
                table: "SensorTypes",
                keyColumn: "Id",
                keyValue: new Guid("f1796a28-642e-401f-8129-fd7465417061"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SensorTypes");

            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "SensorTypes",
                newName: "Name");
        }
    }
}
