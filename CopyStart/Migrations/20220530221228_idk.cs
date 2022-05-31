using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "13d3e0cf-bf89-47d7-bf78-5c1942d313f0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "d74f81d9-4e27-4fb4-ba78-62605c35a022");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "ff27d186-2aa0-44a5-9379-fcd5f8d22d94");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "35003873-9970-43ed-90f0-cf9db36d1c8d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "5ef82162-6e2a-4a82-9608-3543422eacf9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "89441f11-3ff7-4e49-9487-f3733cbb2f0e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "de1cbb74-55e9-4fe7-8b9e-ba30587d526f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "faae5ef3-87a0-4c7f-9e25-07fb0ba659d7");
        }
    }
}
