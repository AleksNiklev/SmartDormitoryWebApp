using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class SeedUSerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"9e5898df-ded2-45cf-ad01-5b1738dd071b", "9597ab59-fa73-4665-957a-220f0c7ab6af"});

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "55704faa-d654-4f78-bc00-7a50145bd4fe", "2b6a1c99-b484-4353-9290-fa36e051802b", "Administrator",
                    "ADMINISTRATOR"
                });

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"3b157522-e71a-492b-b903-c45292464046", "5106cc77-ed7f-4918-a0a8-467c9ae8c962", "User", "USER"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"3b157522-e71a-492b-b903-c45292464046", "5106cc77-ed7f-4918-a0a8-467c9ae8c962"});

            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"55704faa-d654-4f78-bc00-7a50145bd4fe", "2b6a1c99-b484-4353-9290-fa36e051802b"});

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "9e5898df-ded2-45cf-ad01-5b1738dd071b", "9597ab59-fa73-4665-957a-220f0c7ab6af", "Administrator",
                    "ADMINISTRATOR"
                });
        }
    }
}