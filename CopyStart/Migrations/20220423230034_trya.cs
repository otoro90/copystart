using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class trya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.DropIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "ProcedimientoId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_ProcedimientoId",
                table: "ProcedimientosTipoServicios",
                column: "ProcedimientoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.DropIndex(
                name: "IX_ProcedimientosTipoServicios_ProcedimientoId",
                table: "ProcedimientosTipoServicios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedimientosTipoServicios",
                table: "ProcedimientosTipoServicios",
                column: "ProcedimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId",
                table: "ProcedimientosTipoServicios",
                column: "TipoServicioId");
        }
    }
}
