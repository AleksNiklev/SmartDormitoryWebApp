using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class UpdatedSensorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"f31f22a9-35c4-4d42-b56e-bc7ba4eec5bb", "536d14d4-dc13-43e9-be73-d7df46ade901"});

            migrationBuilder.AlterColumn<double>(
                "MinAcceptableValue",
                "SensorTypes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                "MaxAcceptableValue",
                "SensorTypes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "9e5898df-ded2-45cf-ad01-5b1738dd071b", "9597ab59-fa73-4665-957a-220f0c7ab6af", "Administrator",
                    "ADMINISTRATOR"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"9e5898df-ded2-45cf-ad01-5b1738dd071b", "9597ab59-fa73-4665-957a-220f0c7ab6af"});

            migrationBuilder.AlterColumn<int>(
                "MinAcceptableValue",
                "SensorTypes",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                "MaxAcceptableValue",
                "SensorTypes",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "f31f22a9-35c4-4d42-b56e-bc7ba4eec5bb", "536d14d4-dc13-43e9-be73-d7df46ade901", "Administrator",
                    "ADMINISTRATOR"
                });
        }
    }
}