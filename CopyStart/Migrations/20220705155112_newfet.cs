using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class newfet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Ubicaciones_UbicacionId",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_UbicacionId",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Solicitudes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoActivoId",
                table: "TiposServicio",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Servicios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Activos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Activos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "cb412bdc-7e3b-4a0b-b716-742b6638c4e8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "11bd5162-871a-47aa-a0cd-56a974ae26a2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "d3a7b7c4-4a23-4c4a-a78d-d0063a6a07da");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "7595aece-a8e6-4972-9daf-9205a52964c7");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_UbicacionId",
                table: "Activos",
                column: "UbicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activos_Ubicaciones_UbicacionId",
                table: "Activos",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "CodigoLugar",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activos_Ubicaciones_UbicacionId",
                table: "Activos");

            migrationBuilder.DropIndex(
                name: "IX_Activos_UbicacionId",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Activos");

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoActivoId",
                table: "TiposServicio",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Solicitudes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "6cdd8635-66f0-4a3b-8c83-193664022bbd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "4b7dfa9e-22c5-4519-86f3-e0cdaa4ca7cc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "5e25ce51-f000-4fc1-a0dc-b34d1ebec063");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "7c2f9af8-ffd7-45ac-8700-d47899508845");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_UbicacionId",
                table: "Solicitudes",
                column: "UbicacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Ubicaciones_UbicacionId",
                table: "Solicitudes",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "CodigoLugar",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
