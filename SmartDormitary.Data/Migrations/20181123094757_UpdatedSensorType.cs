using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class UpdatedSensorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f31f22a9-35c4-4d42-b56e-bc7ba4eec5bb", "536d14d4-dc13-43e9-be73-d7df46ade901" });

            migrationBuilder.AlterColumn<double>(
                name: "MinAcceptableValue",
                table: "SensorTypes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "MaxAcceptableValue",
                table: "SensorTypes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e5898df-ded2-45cf-ad01-5b1738dd071b", "9597ab59-fa73-4665-957a-220f0c7ab6af", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9e5898df-ded2-45cf-ad01-5b1738dd071b", "9597ab59-fa73-4665-957a-220f0c7ab6af" });

            migrationBuilder.AlterColumn<int>(
                name: "MinAcceptableValue",
                table: "SensorTypes",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "MaxAcceptableValue",
                table: "SensorTypes",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f31f22a9-35c4-4d42-b56e-bc7ba4eec5bb", "536d14d4-dc13-43e9-be73-d7df46ade901", "Administrator", "ADMINISTRATOR" });
        }
    }
}
