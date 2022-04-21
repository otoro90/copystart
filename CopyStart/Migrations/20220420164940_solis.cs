using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class solis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                table: "Servicios");

            migrationBuilder.AlterColumn<Guid>(
                name: "DiagnosticoId",
                table: "Servicios",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "SolicitudId",
                table: "Diagnosticos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_SolicitudId",
                table: "Diagnosticos",
                column: "SolicitudId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosticos_Solicitudes_SolicitudId",
                table: "Diagnosticos",
                column: "SolicitudId",
                principalTable: "Solicitudes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                table: "Servicios",
                column: "DiagnosticoId",
                principalTable: "Diagnosticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosticos_Solicitudes_SolicitudId",
                table: "Diagnosticos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosticos_SolicitudId",
                table: "Diagnosticos");

            migrationBuilder.DropColumn(
                name: "SolicitudId",
                table: "Diagnosticos");

            migrationBuilder.AlterColumn<Guid>(
                name: "DiagnosticoId",
                table: "Servicios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                table: "Servicios",
                column: "DiagnosticoId",
                principalTable: "Diagnosticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
