using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class AddFieldTiempoEjecucionInProcedimientos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TiempoEjecucion",
                table: "Procedimientos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Procedimientos",
                keyColumn: "Id",
                keyValue: new Guid("29c31d04-82cd-4fdb-9470-9fa95a22e2f0"),
                column: "TiempoEjecucion",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Procedimientos",
                keyColumn: "Id",
                keyValue: new Guid("d86abe63-f8ae-4b6c-9ad9-b10db92f9f01"),
                column: "TiempoEjecucion",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "117f86aa-60d5-458c-b004-e297caee8119");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "eb1b4a84-171e-4092-87ee-3a02ba233c29");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "f411b036-53e6-424f-b5a0-536c39118729");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "d680c320-42ef-4c8f-bd23-9f9b00624327");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiempoEjecucion",
                table: "Procedimientos");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "90e138b8-7ee5-486f-bb75-0960b768a9de");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "98002ede-4bb4-4bcd-84e2-578fb9ef0802");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "b77c9af4-b4b7-4c77-a85a-ceb0ce16ab94");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "1d795c9e-aac8-4178-a442-c13a966a476e");
        }
    }
}
