using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class addmigrationfia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProcedimientosTipoServicios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_ProcedimientoId",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "ProcedimientoId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.DropIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_ProcedimientoId",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "ProcedimientoId" });
        }
    }
}
