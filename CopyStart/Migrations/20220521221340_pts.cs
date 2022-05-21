using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class pts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "8ad38c8e-4566-47fa-aa2d-2dda999900a8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "97e9d4e7-cfc0-49de-92f2-e313269b655d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "2bf47368-718f-4646-a099-a49db3f05cf6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "8e55e798-a18b-4f48-a42b-0d73da4d57cf");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_Numero",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "Numero" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_Numero",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "a36eccaf-27d3-4abd-8fd3-97f56f8e8197");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "ebb2aff4-60ca-45d3-9b8b-a6c3fafde092");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "ee16a932-128b-472a-9e51-6e6b8656f947");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "1c9588b7-d2a4-45bb-9d9a-6ea0a44ab32d");
        }
    }
}
