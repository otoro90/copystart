using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class procedi1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TiempoEjecucion",
                table: "Procedimientos",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "a9e9173d-caad-4d98-ba2f-11e0e2053750");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "8841ada6-92a8-4de5-a20d-d0ba1bab7278");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "c3688e40-212c-4738-aa4e-521238d672d8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "c3a6c0c3-38a5-48b8-af4f-a9cfa6dc32b8");
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
                value: "0a548c78-1294-4042-9d0d-903e1aaedc50");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "4af7a042-9601-4f4f-aafe-b451a21e41c5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "3d77805d-a3b3-4e70-a32b-e178aabf2d86");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "9545e303-0a6c-46b6-895c-7291bdac470b");
        }
    }
}
