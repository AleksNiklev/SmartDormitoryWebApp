using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class Admin_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f89f40c7-fc6d-4c23-90a8-e5fd3344eec2", "958be9e1-c5bd-45f4-bf05-b35050c793a4", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "f89f40c7-fc6d-4c23-90a8-e5fd3344eec2", "958be9e1-c5bd-45f4-bf05-b35050c793a4" });
        }
    }
}