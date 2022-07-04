using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class FixFieldDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soportes_Documentos_ReciboId",
                table: "Soportes");

            migrationBuilder.DropIndex(
                name: "IX_Soportes_ReciboId",
                table: "Soportes");

            migrationBuilder.DropColumn(
                name: "ReciboId",
                table: "Soportes");

            migrationBuilder.RenameColumn(
                name: "Descripción",
                table: "Diagnosticos",
                newName: "Descripcion");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "f2d6e525-38f9-4d0e-94a3-c99608960b36");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "09513a91-6a33-417f-a756-c1d1a980230d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "7cbeecdd-6b48-4d5b-8977-a00f7463c3f7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "44ed4cfa-259e-4db8-9680-df631a6e6ed8");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_DocumentoReciboId",
                table: "Soportes",
                column: "DocumentoReciboId");

            migrationBuilder.AddForeignKey(
                name: "FK_Soportes_Documentos_DocumentoReciboId",
                table: "Soportes",
                column: "DocumentoReciboId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soportes_Documentos_DocumentoReciboId",
                table: "Soportes");

            migrationBuilder.DropIndex(
                name: "IX_Soportes_DocumentoReciboId",
                table: "Soportes");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Diagnosticos",
                newName: "Descripción");

            migrationBuilder.AddColumn<Guid>(
                name: "ReciboId",
                table: "Soportes",
                type: "uuid",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_ReciboId",
                table: "Soportes",
                column: "ReciboId");

            migrationBuilder.AddForeignKey(
                name: "FK_Soportes_Documentos_ReciboId",
                table: "Soportes",
                column: "ReciboId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
