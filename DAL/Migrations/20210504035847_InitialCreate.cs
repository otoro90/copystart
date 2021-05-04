using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "SolicitudesServicios",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SerialEquipo = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    NombreCliente = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CelularCliente = table.Column<long>(type: "bigint", nullable: false),
                    DireccionCliente = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesServicios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesServicios",
                schema: "public");
        }
    }
}
