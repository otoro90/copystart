using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class actualizacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosProcedimientoSTipoServicios_ProcedimientosTipoServ~",
                table: "ServiciosProcedimientoSTipoServicios");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosProcedimientoSTipoServicios_Servicios_ServicioId",
                table: "ServiciosProcedimientoSTipoServicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiciosProcedimientoSTipoServicios",
                table: "ServiciosProcedimientoSTipoServicios");

            migrationBuilder.RenameTable(
                name: "ServiciosProcedimientoSTipoServicios",
                newName: "ServiciosProcedimientosTipoServicios");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosProcedimientoSTipoServicios_ServicioId",
                table: "ServiciosProcedimientosTipoServicios",
                newName: "IX_ServiciosProcedimientosTipoServicios_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosProcedimientoSTipoServicios_ProcedimientoTipoServi~",
                table: "ServiciosProcedimientosTipoServicios",
                newName: "IX_ServiciosProcedimientosTipoServicios_ProcedimientoTipoServi~");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiciosProcedimientosTipoServicios",
                table: "ServiciosProcedimientosTipoServicios",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosProcedimientosTipoServicios_ProcedimientosTipoServ~",
                table: "ServiciosProcedimientosTipoServicios",
                column: "ProcedimientoTipoServicioId",
                principalTable: "ProcedimientosTipoServicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosProcedimientosTipoServicios_Servicios_ServicioId",
                table: "ServiciosProcedimientosTipoServicios",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosProcedimientosTipoServicios_ProcedimientosTipoServ~",
                table: "ServiciosProcedimientosTipoServicios");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosProcedimientosTipoServicios_Servicios_ServicioId",
                table: "ServiciosProcedimientosTipoServicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiciosProcedimientosTipoServicios",
                table: "ServiciosProcedimientosTipoServicios");

            migrationBuilder.RenameTable(
                name: "ServiciosProcedimientosTipoServicios",
                newName: "ServiciosProcedimientoSTipoServicios");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosProcedimientosTipoServicios_ServicioId",
                table: "ServiciosProcedimientoSTipoServicios",
                newName: "IX_ServiciosProcedimientoSTipoServicios_ServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiciosProcedimientosTipoServicios_ProcedimientoTipoServi~",
                table: "ServiciosProcedimientoSTipoServicios",
                newName: "IX_ServiciosProcedimientoSTipoServicios_ProcedimientoTipoServi~");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiciosProcedimientoSTipoServicios",
                table: "ServiciosProcedimientoSTipoServicios",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "abf3b387-2c6c-49de-871c-2781b1e6f0fb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "a221ac07-a340-4777-83ef-e89a7455da28");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "9af88b48-61a5-4825-889b-d59e112b011e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "431122f9-218b-48a4-ab34-6ba2b43579d0");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosProcedimientoSTipoServicios_ProcedimientosTipoServ~",
                table: "ServiciosProcedimientoSTipoServicios",
                column: "ProcedimientoTipoServicioId",
                principalTable: "ProcedimientosTipoServicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosProcedimientoSTipoServicios_Servicios_ServicioId",
                table: "ServiciosProcedimientoSTipoServicios",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
