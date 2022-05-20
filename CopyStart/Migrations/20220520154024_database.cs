using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CopyStart.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Peso = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procedimientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Marca = table.Column<string>(type: "text", nullable: false),
                    Modelo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
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
                name: "TiposActivo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposActivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposServicio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    Codigo = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposServicio", x => x.Id);
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
                name: "Soportes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoReciboId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReciboId = table.Column<Guid>(type: "uuid", nullable: true),
                    CopiaFacturaFirmadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentoFacturaId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentoCertificacionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soportes_Documentos_DocumentoCertificacionId",
                        column: x => x.DocumentoCertificacionId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Soportes_Documentos_DocumentoFacturaId",
                        column: x => x.DocumentoFacturaId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Soportes_Documentos_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepuestosProcedimientos",
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
                        principalTable: "Procedimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepuestosProcedimientos_Repuestos_RepuestoId",
                        column: x => x.RepuestoId,
                        principalTable: "Repuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
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
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ProcedimientosTipoServicios",
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
                        principalTable: "Procedimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedimientosTipoServicios_TiposServicio_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalTable: "TiposServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombres = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Apellidos = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TipoDocumentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false),
                    Telefono = table.Column<long>(type: "bigint", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_TiposDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TiposDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personas_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "CodigoLugar",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descripción = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
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
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Serial = table.Column<string>(type: "text", nullable: false),
                    TipoActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    MarcaActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModeloActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PersonaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activos_MarcaActivos_MarcaActivoId",
                        column: x => x.MarcaActivoId,
                        principalTable: "MarcaActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activos_ModeloActivos_ModeloActivoId",
                        column: x => x.ModeloActivoId,
                        principalTable: "ModeloActivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activos_TiposActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalTable: "TiposActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
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
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
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
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
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
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
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
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Incidencia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Descripcion = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    UbicacionId = table.Column<int>(type: "integer", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EstadoSolicitud = table.Column<string>(type: "text", nullable: true),
                    TecnicoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Activos_ActivoId",
                        column: x => x.ActivoId,
                        principalTable: "Activos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Personas_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Personas_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Ubicaciones_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicaciones",
                        principalColumn: "CodigoLugar",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    FechaRealizacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DiagnosticoId = table.Column<Guid>(type: "uuid", nullable: true),
                    ActivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uuid", nullable: false),
                    SoportesId = table.Column<Guid>(type: "uuid", nullable: true),
                    TipoServicioId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicios_Activos_ActivoId",
                        column: x => x.ActivoId,
                        principalTable: "Activos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicios_Diagnosticos_DiagnosticoId",
                        column: x => x.DiagnosticoId,
                        principalTable: "Diagnosticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicios_Soportes_SoportesId",
                        column: x => x.SoportesId,
                        principalTable: "Soportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Servicios_TiposServicio_TipoServicioId",
                        column: x => x.TipoServicioId,
                        principalTable: "TiposServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosProcedimientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProcedimientosRealizados = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosProcedimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiciosProcedimientos_Procedimientos_ProcedimientoId",
                        column: x => x.ProcedimientoId,
                        principalTable: "Procedimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiciosProcedimientos_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ADMIN", "a36eccaf-27d3-4abd-8fd3-97f56f8e8197", "Administrador", "Administrador" },
                    { "COORD", "ee16a932-128b-472a-9e51-6e6b8656f947", "Coordinador", "Coordinador" },
                    { "TEC", "1c9588b7-d2a4-45bb-9d9a-6ea0a44ab32d", "Tecnico", "Tecnico" },
                    { "CLN", "ebb2aff4-60ca-45d3-9b8b-a6c3fafde092", "Cliente", "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "TiposDocumento",
                columns: new[] { "Id", "Codigo", "Descripcion", "Estado", "Nombre" },
                values: new object[] { new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), "CC", "CC", true, "Cedula de Ciudadania" });

            migrationBuilder.InsertData(
                table: "Ubicaciones",
                columns: new[] { "CodigoLugar", "Lugar" },
                values: new object[] { 50001, "Villavicencio" });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Apellidos", "Direccion", "Estado", "Nombres", "NumeroDocumento", "Telefono", "TipoDocumentoId", "UbicacionId" },
                values: new object[,]
                {
                    { new Guid("510c6b6e-a475-488c-9bd9-84a6215da1cb"), "Por defecto", "Calle 40, #33-18", null, "Administrador", "1234567890", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), 50001 },
                    { new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"), "Por defecto", "Calle 40, #33-18", null, "Coordinador", "1234567890", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), 50001 },
                    { new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"), "Por defecto", "Calle 40, #33-18", null, "Tecnico", "1234567890", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), 50001 },
                    { new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"), "Por defecto", "Calle 40, #33-18", null, "Cliente", "1234567890", 3188743948L, new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"), 50001 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonaId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe", 0, "ce435a30-8d16-4770-a7fb-118fab13767b", "user1@gmail.com", true, true, null, "USER1@GMAIL.COM", "USER1@GMAIL.COM", "AQAAAAEAACcQAAAAEBIHivGCDTDdkfSc8TsymI3VtOklfVojN8214LrcTyQWK3lqIRWJ7AHFQMne9rXm0g==", new Guid("510c6b6e-a475-488c-9bd9-84a6215da1cb"), null, false, "SVKHSIPWR4JE76RDYMGA7MQGFIIBR3TX", false, "user1@gmail.com" },
                    { "5c141fdd-ab2f-49be-8a31-43aec63f918f", 0, "776e1d1a-0c3c-41b0-bb44-bcb151add354", "user2@gmail.com", true, true, null, "USER2@GMAIL.COM", "USER2@GMAIL.COM", "AQAAAAEAACcQAAAAEDv98UwloGC1tI3Elt0TgyWk9DxWPi475t9/P2SOAO/TgHilR1/Tnp0kQpnctazOJw==", new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"), null, false, "RZABU4JPNDQVJF4LWR5ZS755BS7H6HDR", false, "user2@gmail.com" },
                    { "3cbfb0a7-d323-4f84-9d14-daeef363f947", 0, "aa35d6da-54e6-476b-8810-4a182b8b07de", "user3@gmail.com", true, true, null, "USER3@GMAIL.COM", "USER3@GMAIL.COM", "AQAAAAEAACcQAAAAEHRn5OJw1NKAwjB4w6dEsVjB2qi/bOR+8/WbmTjTa8lQCvTy6Xg4WqMtm6A/yYYxDw==", new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"), null, false, "4LNTJIEMTON6KDXWASKMDXTFM2BCUCH6", false, "user3@gmail.com" },
                    { "cacd6063-c77d-437e-8cad-2e97ba1a0a6a", 0, "e3330e49-ac28-4952-a50a-a30e8222aed7", "user4@gmail.com", true, true, null, "USER4@GMAIL.COM", "USER4@GMAIL.COM", "AQAAAAEAACcQAAAAEGAsB2jMI6Q3cJTunTexm1lX3qqiVzHMV9sDws+mh3JcqAGY2og313y5uNjZ+32OAg==", new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"), null, false, "WJR7XMGC332UT234BG7O35M6MIIGKQ2Q", false, "user4@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ADMIN", "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe" },
                    { "COORD", "5c141fdd-ab2f-49be-8a31-43aec63f918f" },
                    { "TEC", "3cbfb0a7-d323-4f84-9d14-daeef363f947" },
                    { "CLN", "cacd6063-c77d-437e-8cad-2e97ba1a0a6a" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activos_MarcaActivoId",
                table: "Activos",
                column: "MarcaActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_ModeloActivoId",
                table: "Activos",
                column: "ModeloActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_PersonaId",
                table: "Activos",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Activos_TipoActivoId",
                table: "Activos",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonaId",
                table: "AspNetUsers",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_SoportesId",
                table: "Diagnosticos",
                column: "SoportesId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcaActivos_TipoActivoId",
                table: "MarcaActivos",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloActivos_MarcaActivoId",
                table: "ModeloActivos",
                column: "MarcaActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TipoDocumentoId",
                table: "Personas",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UbicacionId",
                table: "Personas",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_ProcedimientoId",
                table: "ProcedimientosTipoServicios",
                column: "ProcedimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedimientosTipoServicios_TipoServicioId_ProcedimientoId",
                table: "ProcedimientosTipoServicios",
                columns: new[] { "TipoServicioId", "ProcedimientoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProcedimientos_ProcedimientoId",
                table: "RepuestosProcedimientos",
                column: "ProcedimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProcedimientos_RepuestoId",
                table: "RepuestosProcedimientos",
                column: "RepuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_ActivoId",
                table: "Servicios",
                column: "ActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_DiagnosticoId",
                table: "Servicios",
                column: "DiagnosticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_SolicitudId",
                table: "Servicios",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_SoportesId",
                table: "Servicios",
                column: "SoportesId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosProcedimientos_ProcedimientoId",
                table: "ServiciosProcedimientos",
                column: "ProcedimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosProcedimientos_ServicioId",
                table: "ServiciosProcedimientos",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ActivoId",
                table: "Solicitudes",
                column: "ActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_ClienteId",
                table: "Solicitudes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_TecnicoId",
                table: "Solicitudes",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_UbicacionId",
                table: "Solicitudes",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_DocumentoCertificacionId",
                table: "Soportes",
                column: "DocumentoCertificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_DocumentoFacturaId",
                table: "Soportes",
                column: "DocumentoFacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Soportes_ReciboId",
                table: "Soportes",
                column: "ReciboId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedimientosTipoServicios");

            migrationBuilder.DropTable(
                name: "RepuestosProcedimientos");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "ServiciosProcedimientos");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Repuestos");

            migrationBuilder.DropTable(
                name: "Procedimientos");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "TiposServicio");

            migrationBuilder.DropTable(
                name: "Soportes");

            migrationBuilder.DropTable(
                name: "Activos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "ModeloActivos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "MarcaActivos");

            migrationBuilder.DropTable(
                name: "TiposDocumento");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "TiposActivo");
        }
    }
}
