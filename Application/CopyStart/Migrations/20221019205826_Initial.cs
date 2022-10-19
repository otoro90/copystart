using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CopyStart.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Archivos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Peso = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcaActivos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaActivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procedimientos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TiempoEjecucion = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repuestos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Marca = table.Column<string>(type: "text", nullable: false),
                    Modelo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                schema: "public",
                columns: table => new
                {
                    CodigoMunicipio = table.Column<string>(type: "text", nullable: false),
                    CodigoDepartamento = table.Column<string>(type: "text", nullable: true),
                    Departamento = table.Column<string>(type: "text", nullable: false),
                    Municipio = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<string>(type: "text", nullable: true),
                    Longitud = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.CodigoMunicipio);
                });

            migrationBuilder.CreateTable(
                name: "TiposActivo",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MarcaActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposActivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposActivo_MarcaActivos_MarcaActivoId",
                        column: x => x.MarcaActivoId,
                        principalSchema: "public",
                        principalTable: "MarcaActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepuestosProcedimientos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    RepuestoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepuestosProcedimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepuestosProcedimientos_Procedimientos_ProcedimientoId",
                        column: x => x.ProcedimientoId,
                        principalSchema: "public",
                        principalTable: "Procedimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepuestosProcedimientos_Repuestos_RepuestoId",
                        column: x => x.RepuestoId,
                        principalSchema: "public",
                        principalTable: "Repuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombres = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Apellidos = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TipoDocumentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UbicacionId = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<long>(type: "bigint", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_TiposDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalSchema: "public",
                        principalTable: "TiposDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalSchema: "public",
                        principalTable: "Ubicaciones",
                        principalColumn: "CodigoMunicipio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloActivos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloActivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeloActivos_TiposActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalSchema: "public",
                        principalTable: "TiposActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposServicio",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposServicio_TiposActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalSchema: "public",
                        principalTable: "TiposActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalSchema: "public",
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Activos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Serial = table.Column<string>(type: "text", nullable: true),
                    TipoActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    MarcaActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModeloActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UbicacionId = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activos_MarcaActivos_MarcaActivoId",
                        column: x => x.MarcaActivoId,
                        principalSchema: "public",
                        principalTable: "MarcaActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activos_ModeloActivos_ModeloActivoId",
                        column: x => x.ModeloActivoId,
                        principalSchema: "public",
                        principalTable: "ModeloActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalSchema: "public",
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activos_TiposActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalSchema: "public",
                        principalTable: "TiposActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activos_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalSchema: "public",
                        principalTable: "Ubicaciones",
                        principalColumn: "CodigoMunicipio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcedimientosTipoServicios",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    ProcedimientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoServicioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedimientosTipoServicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedimientosTipoServicios_Procedimientos_ProcedimientoId",
                        column: x => x.ProcedimientoId,
                        principalSchema: "public",
                        principalTable: "Procedimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcedimientosTipoServicios_TiposServicio_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalSchema: "public",
                        principalTable: "TiposServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Incidencia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    MotivoCancelacion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoSolicitud = table.Column<string>(type: "text", nullable: true),
                    TecnicoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivoId = table.Column<long>(type: "bigint", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Activos_ActivoId",
                        column: x => x.ActivoId,
                        principalSchema: "public",
                        principalTable: "Activos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Personas_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "public",
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Personas_TecnicoId",
                        column: x => x.TecnicoId,
                        principalSchema: "public",
                        principalTable: "Personas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaFinalizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActivoId = table.Column<long>(type: "bigint", nullable: false),
                    SolicitudId = table.Column<long>(type: "bigint", nullable: false),
                    TipoServicioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicios_Activos_ActivoId",
                        column: x => x.ActivoId,
                        principalSchema: "public",
                        principalTable: "Activos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalSchema: "public",
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_TiposServicio_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalSchema: "public",
                        principalTable: "TiposServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArchivoServicio",
                schema: "public",
                columns: table => new
                {
                    ArchivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicioId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivoServicio", x => new { x.ServicioId, x.ArchivoId });
                    table.ForeignKey(
                        name: "FK_ArchivoServicio_Archivos_ArchivoId",
                        column: x => x.ArchivoId,
                        principalSchema: "public",
                        principalTable: "Archivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArchivoServicio_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalSchema: "public",
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosProcedimientosTipoServicios",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicioId = table.Column<long>(type: "bigint", nullable: false),
                    ProcedimientoTipoServicioId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimientosRealizados = table.Column<bool>(type: "boolean", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosProcedimientosTipoServicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiciosProcedimientosTipoServicios_ProcedimientosTipoServ~",
                        column: x => x.ProcedimientoTipoServicioId,
                        principalSchema: "public",
                        principalTable: "ProcedimientosTipoServicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiciosProcedimientosTipoServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalSchema: "public",
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ADMIN", "2dba45d4-f60a-4e63-b08d-6f947902b2ab", "Administrador", "Administrador" },
                    { "CLN", "298b44be-1740-4643-8a18-5bef832683c9", "Cliente", "Cliente" },
                    { "COORD", "cb08b4e0-4697-4cf8-bf5d-1f74e0d8590c", "Coordinador", "Coordinador" },
                    { "TEC", "6cff4868-ef07-4cc5-badf-7208d7dfdbf3", "Tecnico", "Tecnico" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "TiposDocumento",
                columns: new[] { "Id", "Codigo", "Descripcion", "Estado", "Nombre" },
                values: new object[] { new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), "CC", "CC", true, "Cedula de Ciudadania" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Ubicaciones",
                columns: new[] { "CodigoMunicipio", "CodigoDepartamento", "Departamento", "Latitud", "Longitud", "Municipio" },
                values: new object[] { "50001", "50", "META", "4,09166877", "-73,492915945", "VILLAVICENCIO" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Personas",
                columns: new[] { "Id", "Apellidos", "Direccion", "Estado", "Nombres", "NumeroDocumento", "Telefono", "TipoDocumentoId", "UbicacionId" },
                values: new object[] { new Guid("510c6b6e-a475-488c-9bd9-84a6215da1cb"), "Por defecto", "Calle 40, #33-18", "Activo", "Administrador", "1000000000", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), "50001" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonaId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe", 0, "ce435a30-8d16-4770-a7fb-118fab13767b", "cpadmin@yopmail.com", true, true, null, "CPADMIN@YOPMAIL.COM", "CPADMIN@YOPMAIL.COM", "AQAAAAEAACcQAAAAEBIHivGCDTDdkfSc8TsymI3VtOklfVojN8214LrcTyQWK3lqIRWJ7AHFQMne9rXm0g==", new Guid("510c6b6e-a475-488c-9bd9-84a6215da1cb"), null, false, "SVKHSIPWR4JE76RDYMGA7MQGFIIBR3TX", false, "cpadmin@yopmail.com" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ADMIN", "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe" });

            migrationBuilder.CreateIndex(
                name: "IX_Activos_Id_Serial",
                schema: "public",
                table: "Activos",
                columns: new[] { "Id", "Serial" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activos_MarcaActivoId",
                schema: "public",
                table: "Activos",
                column: "MarcaActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_ModeloActivoId",
                schema: "public",
                table: "Activos",
                column: "ModeloActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_PersonaId",
                schema: "public",
                table: "Activos",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_TipoActivoId",
                schema: "public",
                table: "Activos",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_UbicacionId",
                schema: "public",
                table: "Activos",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivoServicio_ArchivoId",
                schema: "public",
                table: "ArchivoServicio",
                column: "ArchivoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonaId",
                schema: "public",
                table: "AspNetUsers",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "public",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModeloActivos_TipoActivoId",
                schema: "public",
                table: "ModeloActivos",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_NumeroDocumento",
                schema: "public",
                table: "Personas",
                column: "NumeroDocumento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoDocumentoId",
                schema: "public",
                table: "Personas",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UbicacionId",
                schema: "public",
                table: "Personas",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_ProcedimientoId",
                schema: "public",
                table: "ProcedimientosTipoServicios",
                column: "ProcedimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_Numero",
                schema: "public",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_ProcedimientoId",
                schema: "public",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "ProcedimientoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProcedimientos_ProcedimientoId",
                schema: "public",
                table: "RepuestosProcedimientos",
                column: "ProcedimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProcedimientos_RepuestoId",
                schema: "public",
                table: "RepuestosProcedimientos",
                column: "RepuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "public",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "public",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_ActivoId",
                schema: "public",
                table: "Servicios",
                column: "ActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_SolicitudId",
                schema: "public",
                table: "Servicios",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TipoServicioId",
                schema: "public",
                table: "Servicios",
                column: "TipoServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosProcedimientosTipoServicios_ProcedimientoTipoServi~",
                schema: "public",
                table: "ServiciosProcedimientosTipoServicios",
                column: "ProcedimientoTipoServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosProcedimientosTipoServicios_ServicioId",
                schema: "public",
                table: "ServiciosProcedimientosTipoServicios",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ActivoId",
                schema: "public",
                table: "Solicitudes",
                column: "ActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ClienteId",
                schema: "public",
                table: "Solicitudes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_TecnicoId",
                schema: "public",
                table: "Solicitudes",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposActivo_MarcaActivoId_Nombre",
                schema: "public",
                table: "TiposActivo",
                columns: new[] { "MarcaActivoId", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposServicio_TipoActivoId",
                schema: "public",
                table: "TiposServicio",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "public",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "public",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "public",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivoServicio",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RepuestosProcedimientos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ServiciosProcedimientosTipoServicios",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Archivos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Repuestos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProcedimientosTipoServicios",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Servicios",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Procedimientos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Solicitudes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TiposServicio",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Activos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ModeloActivos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Personas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TiposActivo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "TiposDocumento",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Ubicaciones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MarcaActivos",
                schema: "public");
        }
    }
}
