using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class idkerror : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Serial",
                table: "Activos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "f33764ec-71e5-4e5d-b2ea-447a9e116717");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "790fe0fc-067a-4d47-bc04-13e322305ec4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "089c613c-b6c4-4a70-86a3-7f1165096081");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "5d70486b-abee-4de5-8390-b915ef262700");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Serial",
                table: "Activos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "5ec78bd8-49cf-4a34-8331-23d2ae65f00a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "8dce1b1b-fdb0-4450-9e4b-4ae223763195");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "59bdabce-1106-4f5d-b0de-4a2840ff1735");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "e61d4514-0b52-4dee-b375-c51177f4d35e");
        }
    }
}
