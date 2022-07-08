using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CopyStart.Migrations
{
    public partial class btw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                table: "Servicios");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_DiagnosticoId",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "DiagnosticoId",
                table: "Servicios");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Servicios",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinalizacion",
                table: "Servicios",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "5ec78bd8-49cf-4a34-8331-23d2ae65f00a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "8dce1b1b-fdb0-4450-9e4b-4ae223763195");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "59bdabce-1106-4f5d-b0de-4a2840ff1735");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "e61d4514-0b52-4dee-b375-c51177f4d35e");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_Id_Serial",
                table: "Activos",
                columns: new[] { "Id", "Serial" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Activos_Id_Serial",
                table: "Activos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaInicio",
                table: "Servicios",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinalizacion",
                table: "Servicios",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DiagnosticoId",
                table: "Servicios",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    FechaDiagnostico = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SoportesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosticos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnosticos_Soportes_SoportesId",
                        column: x => x.SoportesId,
                        principalTable: "Soportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ADMIN",
                column: "ConcurrencyStamp",
                value: "cb412bdc-7e3b-4a0b-b716-742b6638c4e8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "CLN",
                column: "ConcurrencyStamp",
                value: "11bd5162-871a-47aa-a0cd-56a974ae26a2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "COORD",
                column: "ConcurrencyStamp",
                value: "d3a7b7c4-4a23-4c4a-a78d-d0063a6a07da");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "TEC",
                column: "ConcurrencyStamp",
                value: "7595aece-a8e6-4972-9daf-9205a52964c7");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_DiagnosticoId",
                table: "Servicios",
                column: "DiagnosticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_SoportesId",
                table: "Diagnosticos",
                column: "SoportesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                table: "Servicios",
                column: "DiagnosticoId",
                principalTable: "Diagnosticos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
