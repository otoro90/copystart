using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CopyStart.Migrations
{
    public partial class newentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcaActivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaActivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarcaActivos_TiposActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalTable: "TiposActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    CodigoLugar = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lugar = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.CodigoLugar);
                });

            migrationBuilder.CreateTable(
                name: "ModeloActivos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MarcaActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloActivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloActivos_MarcaActivos_MarcaActivoId",
                        column: x => x.MarcaActivoId,
                        principalTable: "MarcaActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarcaActivos_TipoActivoId",
                table: "MarcaActivos",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloActivos_MarcaActivoId",
                table: "ModeloActivos",
                column: "MarcaActivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModeloActivos");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "MarcaActivos");
        }
    }
}
