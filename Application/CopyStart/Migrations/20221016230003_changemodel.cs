using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class changemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcaActivos_TiposActivo_TipoActivoId",
                table: "MarcaActivos");

            migrationBuilder.DropIndex(
                name: "IX_MarcaActivos_TipoActivoId",
                table: "MarcaActivos");

            migrationBuilder.DropColumn(
                name: "TipoActivoId",
                table: "MarcaActivos");

            migrationBuilder.AddColumn<Guid>(
                name: "MarcaActivoId",
                table: "TiposActivo",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "7760a243-8076-4c1a-8566-c6b766840c85");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "49c07024-eb7a-482a-ac0c-957caf51fe54");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "ac8a5120-13c1-4623-b05b-c2a7648f2765");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "4fbf23fb-33b4-47f0-804d-80718fffc5d1");

            migrationBuilder.CreateIndex(
                name: "IX_TiposActivo_MarcaActivoId",
                table: "TiposActivo",
                column: "MarcaActivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposActivo_MarcaActivos_MarcaActivoId",
                table: "TiposActivo",
                column: "MarcaActivoId",
                principalTable: "MarcaActivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TiposActivo_MarcaActivos_MarcaActivoId",
                table: "TiposActivo");

            migrationBuilder.DropIndex(
                name: "IX_TiposActivo_MarcaActivoId",
                table: "TiposActivo");

            migrationBuilder.DropColumn(
                name: "MarcaActivoId",
                table: "TiposActivo");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoActivoId",
                table: "MarcaActivos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "65360229-128c-440a-8e11-f10292071605");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "8690d3af-358c-4018-9add-996257fb63e5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "5e3ea73e-397c-405b-aea3-e38f18ea42bf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "63be5bbf-55f4-47c6-8227-bba33bf112e2");

            migrationBuilder.CreateIndex(
                name: "IX_MarcaActivos_TipoActivoId",
                table: "MarcaActivos",
                column: "TipoActivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcaActivos_TiposActivo_TipoActivoId",
                table: "MarcaActivos",
                column: "TipoActivoId",
                principalTable: "TiposActivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
