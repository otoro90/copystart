using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class ae3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosticos_TiposServicio_TipoServicioId",
                table: "Diagnosticos");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosticos_TipoServicioId",
                table: "Diagnosticos");

            migrationBuilder.DropColumn(
                name: "TipoServicioId",
                table: "Diagnosticos");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoServicioId",
                table: "Servicios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_TiposServicio_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId",
                principalTable: "TiposServicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_TiposServicio_TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "TipoServicioId",
                table: "Servicios");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoServicioId",
                table: "Diagnosticos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_TipoServicioId",
                table: "Diagnosticos",
                column: "TipoServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosticos_TiposServicio_TipoServicioId",
                table: "Diagnosticos",
                column: "TipoServicioId",
                principalTable: "TiposServicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
