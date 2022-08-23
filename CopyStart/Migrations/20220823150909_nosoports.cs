using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CopyStart.Migrations
{
    public partial class nosoports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Soportes_SoportesId",
                table: "Servicios");

            migrationBuilder.DropTable(
                name: "Soportes");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_SoportesId",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "SoportesId",
                table: "Servicios");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "41d1931a-8e90-4eff-a4d8-754afcd7490e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "d644881f-0727-4ce2-b9eb-9cbc0ee1ca85");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "6b073d77-a8ff-4802-8296-8d0acdd54ffb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "6c298094-8d98-4301-80f2-ba66bc1e4fb3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SoportesId",
                table: "Servicios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Soportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CopiaFacturaFirmadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoCertificacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoFacturaId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentoReciboId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soportes_Documentos_DocumentoCertificacionId",
                        column: x => x.DocumentoCertificacionId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Soportes_Documentos_DocumentoFacturaId",
                        column: x => x.DocumentoFacturaId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Soportes_Documentos_DocumentoReciboId",
                        column: x => x.DocumentoReciboId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "29a9a399-8ebb-4908-97e0-d327eca7c03b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "3bbf087e-0962-4290-921a-90b8bbf3f904");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "31996576-135b-4aca-ac0d-ff0ad8b9d68e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "2454bcdc-6b82-4360-af62-e2fd1b015390");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_SoportesId",
                table: "Servicios",
                column: "SoportesId");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_DocumentoCertificacionId",
                table: "Soportes",
                column: "DocumentoCertificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_DocumentoFacturaId",
                table: "Soportes",
                column: "DocumentoFacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_DocumentoReciboId",
                table: "Soportes",
                column: "DocumentoReciboId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Soportes_SoportesId",
                table: "Servicios",
                column: "SoportesId",
                principalTable: "Soportes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
