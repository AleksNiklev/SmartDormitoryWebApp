using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartDormitary.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_AspNetUsers_UserId1",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_UserId1",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Sensors");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Sensors",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_UserId",
                table: "Sensors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_AspNetUsers_UserId",
                table: "Sensors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_AspNetUsers_UserId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_UserId",
                table: "Sensors");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Sensors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Sensors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_UserId1",
                table: "Sensors",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_AspNetUsers_UserId1",
                table: "Sensors",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
