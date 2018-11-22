using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "9f2f7134-e1f3-40ac-9d7f-2e451d9f8969", "987e7813-9286-487a-8b33-89fcedd9b1c6" });

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Sensors",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f31f22a9-35c4-4d42-b56e-bc7ba4eec5bb", "536d14d4-dc13-43e9-be73-d7df46ade901", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f31f22a9-35c4-4d42-b56e-bc7ba4eec5bb", "536d14d4-dc13-43e9-be73-d7df46ade901" });

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Sensors",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f2f7134-e1f3-40ac-9d7f-2e451d9f8969", "987e7813-9286-487a-8b33-89fcedd9b1c6", "Administrator", "ADMINISTRATOR" });
        }
    }
}
