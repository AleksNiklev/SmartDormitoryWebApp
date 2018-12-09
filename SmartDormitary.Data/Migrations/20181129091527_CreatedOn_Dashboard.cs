using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class CreatedOn_Dashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"3b157522-e71a-492b-b903-c45292464046", "5106cc77-ed7f-4918-a0a8-467c9ae8c962"});

            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"55704faa-d654-4f78-bc00-7a50145bd4fe", "2b6a1c99-b484-4353-9290-fa36e051802b"});

            migrationBuilder.AddColumn<DateTime>(
                "CreatedOn",
                "Sensors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                "CreatedOn",
                "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                {
                    "759094a7-e427-4794-ba83-33bdcf3fe64a", "a9b16ad2-01e4-41d5-8e42-430249b32960", "Administrator",
                    "ADMINISTRATOR"
                });

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[]
                    {"72c3f103-7479-464b-9a72-6e8b982fc71d", "8bfb962e-c8a6-4498-b482-01fa080fa42b", "User", "USER"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"72c3f103-7479-464b-9a72-6e8b982fc71d", "8bfb962e-c8a6-4498-b482-01fa080fa42b"});

            migrationBuilder.DeleteData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp"},
                new object[] {"759094a7-e427-4794-ba83-33bdcf3fe64a", "a9b16ad2-01e4-41d5-8e42-430249b32960"});

            migrationBuilder.DropColumn(
                "CreatedOn",
                "Sensors");

            migrationBuilder.DropColumn(
                "CreatedOn",
                "AspNetUsers");

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
    }
}