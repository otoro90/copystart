using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class solbug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosticos_Solicitudes_SolicitudId",
                table: "Diagnosticos");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosticos_SolicitudId",
                table: "Diagnosticos");

            migrationBuilder.DropColumn(
                name: "SolicitudId",
                table: "Diagnosticos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
