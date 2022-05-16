using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class activo4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Activos");

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Solicitudes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UbicacionId",
                table: "Personas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "MarcaActivoId",
                table: "Activos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModeloActivoId",
                table: "Activos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_UbicacionId",
                table: "Solicitudes",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UbicacionId",
                table: "Personas",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_MarcaActivoId",
                table: "Activos",
                column: "MarcaActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_ModeloActivoId",
                table: "Activos",
                column: "ModeloActivoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activos_MarcaActivos_MarcaActivoId",
                table: "Activos",
                column: "MarcaActivoId",
                principalTable: "MarcaActivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activos_ModeloActivos_ModeloActivoId",
                table: "Activos",
                column: "ModeloActivoId",
                principalTable: "ModeloActivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionId",
                table: "Personas",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "CodigoLugar",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Ubicaciones_UbicacionId",
                table: "Solicitudes",
                column: "UbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "CodigoLugar",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activos_MarcaActivos_MarcaActivoId",
                table: "Activos");

            migrationBuilder.DropForeignKey(
                name: "FK_Activos_ModeloActivos_ModeloActivoId",
                table: "Activos");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Ubicaciones_UbicacionId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Ubicaciones_UbicacionId",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_UbicacionId",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_Personas_UbicacionId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Activos_MarcaActivoId",
                table: "Activos");

            migrationBuilder.DropIndex(
                name: "IX_Activos_ModeloActivoId",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "UbicacionId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "MarcaActivoId",
                table: "Activos");

            migrationBuilder.DropColumn(
                name: "ModeloActivoId",
                table: "Activos");

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Solicitudes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Personas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Activos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Activos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Activos",
                type: "text",
                nullable: true);
        }
    }
}
