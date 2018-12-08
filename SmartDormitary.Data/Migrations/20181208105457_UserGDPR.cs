using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class UserGDPR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "fd9823f6-43ec-464e-8ee7-a29212fe779c", "e3a1a418-5641-4474-bcdb-b42079f8fa30" });

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedGDPR",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52afd0fb-1c27-440a-a0b1-a1e6ad474459", "26571fef-9b7d-48f6-bf6e-0eb10c0b3a58", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "52afd0fb-1c27-440a-a0b1-a1e6ad474459", "26571fef-9b7d-48f6-bf6e-0eb10c0b3a58" });

            migrationBuilder.DropColumn(
                name: "AcceptedGDPR",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd9823f6-43ec-464e-8ee7-a29212fe779c", "e3a1a418-5641-4474-bcdb-b42079f8fa30", "Administrator", "ADMINISTRATOR" });
        }
    }
}
