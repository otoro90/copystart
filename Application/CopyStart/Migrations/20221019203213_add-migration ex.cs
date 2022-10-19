using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class addmigrationex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TiposActivo_MarcaActivoId",
                table: "TiposActivo");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "e2ca9015-13b8-45bf-8a6a-b33a574a278d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "92658845-86dd-4a98-b98a-265dfbe65af3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "2490e8bf-c4f9-465b-beef-0ba6a3c61201");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "42aa164f-5449-489e-ad9e-45c3bf8c5abb");

            migrationBuilder.CreateIndex(
                name: "IX_TiposActivo_MarcaActivoId_Nombre",
                table: "TiposActivo",
                columns: new[] { "MarcaActivoId", "Nombre" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TiposActivo_MarcaActivoId_Nombre",
                table: "TiposActivo");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "860d39f9-8367-4875-9f9d-25a39e97162f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "d92b89bf-9634-447f-ab08-570af3319755");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "4b3839d3-5608-4a32-9d86-f895237a09c5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "0c10b08e-46d4-4422-8e72-ca71428a425c");

            migrationBuilder.CreateIndex(
                name: "IX_TiposActivo_MarcaActivoId",
                table: "TiposActivo",
                column: "MarcaActivoId");
        }
    }
}
