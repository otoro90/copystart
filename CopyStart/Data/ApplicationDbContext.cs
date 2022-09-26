using System;
using System.Linq;
using CopyStart.Entities;
using CopyStart.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CopyStart.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {

        public IConfiguration configuration { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            configuration = AppSettings.GetConfiguration();
        }


        public virtual DbSet<Activo> Activo { get; set; }
        public virtual DbSet<Archivo> Archivo { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Procedimiento> Procedimiento { get; set; }
        public virtual DbSet<ProcedimientoTipoServicio> ProcedimientoTipoServicio { get; set; }
        public virtual DbSet<Repuesto> Repuesto { get; set; }
        public virtual DbSet<RepuestoProcedimiento> RepuestoProcedimiento { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<ServicioProcedimientoTipoServicio> ServicioProcedimientoTipoServicio { get; set; }
        public virtual DbSet<ArchivoServicio> ArchivoServicio { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<TipoServicio> TipoServicio { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoActivo> TipoActivo { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
        public virtual DbSet<MarcaActivo> MarcaActivo { get; set; }
        public virtual DbSet<ModeloActivo> ModeloActivo { get; set; }
        public virtual DbSet<ApplicationUser> User { get; set; }
        public virtual DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }
        public virtual DbSet<ApplicationUserLogin> ApplicationUserLogin { get; set; }
        public virtual DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }
        public virtual DbSet<ApplicationRoleClaim> ApplicationRoleClaim { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DBConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProcedimientoTipoServicio>()
            .HasIndex(p => new { p.TipoServicioId, p.ProcedimientoId })
            .IsUnique(true);

            modelBuilder.Entity<ProcedimientoTipoServicio>()
            .HasIndex(p => new { p.TipoServicioId, p.Numero })
            .IsUnique(true);

            modelBuilder.Entity<Activo>()
              .HasIndex(p => new { p.Id, p.Serial })
              .IsUnique(true);

            modelBuilder.Entity<ArchivoServicio>().HasKey(p => new { p.ServicioId, p.ArchivoId });          


            modelBuilder.Entity<Persona>()
             .HasIndex(p => new { p.NumeroDocumento })
             .IsUnique(true);


            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Activo>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<Solicitud>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Servicio>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd()
            ;

            modelBuilder.Entity<ServicioProcedimientoTipoServicio>(entity =>
            {
                entity.HasOne(e => e.Servicio)
                       .WithMany(r => r.ProcedimientosRealizados)
                       .HasForeignKey(ur => ur.ServicioId);
            });

            modelBuilder.Entity<Servicio>();

            modelBuilder.Entity<TipoDocumento>(b =>
            {
                b.HasData(new TipoDocumento()
                {
                    Id = new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    Nombre = "Cedula de Ciudadania",
                    Codigo = "CC",
                    Descripcion = "CC",
                    Estado = true
                });
            });

            modelBuilder.Entity<TipoServicio>(b =>
            {
                b.HasData(new TipoServicio()
                {
                    Id = new Guid("64b5a31e-0d62-42d4-82d2-2eef76330d9d"),
                    Nombre = "Mantenimiento de impresora",
                    Codigo = "PCMAN",
                    Descripcion = "",
                    Estado = true,
                    TipoActivoId = Guid.Parse("f443468f-26a6-4e4c-a9c5-77de2953f802")

                });
            });


            modelBuilder.Entity<TipoActivo>(b =>
            {
                b.HasData(new TipoActivo()
                {
                    Id = new Guid("f443468f-26a6-4e4c-a9c5-77de2953f802"),
                    Nombre = "Impresora",
                    Codigo = "IMP",
                    Descripcion = "",
                    Estado = true
                });
            });

            modelBuilder.Entity<MarcaActivo>(b =>
            {
                b.HasData(new MarcaActivo()
                {
                    Id = new Guid("0b5de1c4-7bd1-444f-8f22-cab2cc0dcfdd"),
                    TipoActivoId = Guid.Parse("f443468f-26a6-4e4c-a9c5-77de2953f802"),
                    Nombre = "Ricoh",
                    Codigo = "RICOH",
                    Descripcion = "",
                    Estado = true
                });
            });


            modelBuilder.Entity<ModeloActivo>(b =>
            {
                b.HasData(new ModeloActivo()
                {
                    Id = new Guid("6ca30cec-d049-4144-8cf1-6ec4e90f4b57"),
                    MarcaActivoId = Guid.Parse("0b5de1c4-7bd1-444f-8f22-cab2cc0dcfdd"),
                    Nombre = "Aficio 2345",
                    Codigo = "A2345",
                    Descripcion = "",
                    Estado = true
                });
            });

            modelBuilder.Entity<Procedimiento>(b =>
            {
                b.HasData(
                    new Procedimiento()
                    {
                        Id = new Guid("29c31d04-82cd-4fdb-9470-9fa95a22e2f0"),
                        Nombre = "Abrir maquina",
                        Codigo = "AMAQ",
                        Descripcion = "",
                        TiempoEjecucion = 10,
                        Estado = true
                    }, new Procedimiento()
                    {
                        Id = new Guid("d86abe63-f8ae-4b6c-9ad9-b10db92f9f01"),
                        Nombre = "Revisar componentes",
                        Codigo = "RCOMP",
                        Descripcion = "",
                        TiempoEjecucion = 20,
                        Estado = true
                    }
                    );
            });

            modelBuilder.Entity<ProcedimientoTipoServicio>(b =>
            {
                b.HasData(
                    new ProcedimientoTipoServicio()
                    {
                        Id = new Guid("2f3a8930-b64a-4cd8-beec-0c539551ea6a"),
                        Numero = 1,
                        ProcedimientoId = Guid.Parse("29c31d04-82cd-4fdb-9470-9fa95a22e2f0"),
                        TipoServicioId = Guid.Parse("64b5a31e-0d62-42d4-82d2-2eef76330d9d")

                    }, new ProcedimientoTipoServicio()
                    {
                        Id = new Guid("a35d9139-39dd-4fa2-b1a4-9dc64358c27a"),
                        Numero = 2,
                        ProcedimientoId = Guid.Parse("d86abe63-f8ae-4b6c-9ad9-b10db92f9f01"),
                        TipoServicioId = Guid.Parse("64b5a31e-0d62-42d4-82d2-2eef76330d9d")
                    }
                    );
            });




            modelBuilder.Entity<Ubicacion>(b =>
            {
                b.HasData(new Ubicacion()
                {
                    CodigoDepartamento = "50",
                    Departamento = "META",
                    CodigoMunicipio = "50001",
                    Municipio = "VILLAVICENCIO",
                    Latitud = "4,09166877",
                    Longitud = "-73,492915945"
                });
            });

            modelBuilder.Entity<Persona>(b =>
            {
                // Each Persona can have many Users
                b.HasMany(e => e.Users)
                    .WithOne(e => e.Persona)
                    .HasForeignKey(ul => ul.PersonaId);
                b.HasData(new Persona()
                {
                    Id = new Guid("510c6b6e-a475-488c-9bd9-84a6215da1cb"),
                    Nombres = "Administrador",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1000000000",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = "50001",
                    Estado = "Activo"
                }, new Persona()
                {
                    Id = new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"),
                    Nombres = "Coordinador",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1000000001",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = "50001",
                    Estado = "Activo"
                }, new Persona()
                {
                    Id = new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"),
                    Nombres = "Tecnico",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1000000002",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = "50001",
                    Estado = "Disponible"


                }
                , new Persona()
                {
                    Id = new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"),
                    Nombres = "Cliente",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1000000003",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = "50001",
                    Estado="Activo"
                    
                });
            });

            modelBuilder.Entity<ApplicationUserClaim>(b =>
            {
                b.ToTable("UserClaims");
            });

            modelBuilder.Entity<ApplicationUserLogin>(b =>
            {
                b.ToTable("UserLogins");
            });

            modelBuilder.Entity<ApplicationUserToken>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");
                b.HasData(new ApplicationRole()
                {
                    Id = "ADMIN",
                    Name = "Administrador",
                    NormalizedName = "Administrador"
                }, new ApplicationRole()
                {
                    Id = "COORD",
                    Name = "Coordinador",
                    NormalizedName = "Coordinador"
                }, new ApplicationRole()
                {
                    Id = "TEC",
                    Name = "Tecnico",
                    NormalizedName = "Tecnico"
                }, new ApplicationRole()
                {
                    Id = "CLN",
                    Name = "Cliente",
                    NormalizedName = "Cliente"
                });
            });

            modelBuilder.Entity<ApplicationRoleClaim>(b =>
            {
                b.ToTable("RoleClaims");
            });

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("UserRoles");
                b.HasData(
                    new ApplicationUserRole()
                    {
                        RoleId = "ADMIN",
                        UserId = "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe"
                    }, new ApplicationUserRole()
                    {
                        RoleId = "COORD",
                        UserId = "5c141fdd-ab2f-49be-8a31-43aec63f918f"
                    }, new ApplicationUserRole()
                    {
                        RoleId = "TEC",
                        UserId = "3cbfb0a7-d323-4f84-9d14-daeef363f947"
                    }, new ApplicationUserRole()
                    {
                        RoleId = "CLN",
                        UserId = "cacd6063-c77d-437e-8cad-2e97ba1a0a6a"
                    });
            });

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasData(new ApplicationUser()
                {
                    Id = "1cf68c49-edd7-4d24-ab0c-b14c6aef0ebe",
                    PersonaId = Guid.Parse("510c6b6e-a475-488c-9bd9-84a6215da1cb"),
                    UserName = "cpadmin@yopmail.com",
                    NormalizedUserName = "CPADMIN@YOPMAIL.COM",
                    Email = "cpadmin@gmail.com",
                    NormalizedEmail = "CPADMIN@YOPMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEBIHivGCDTDdkfSc8TsymI3VtOklfVojN8214LrcTyQWK3lqIRWJ7AHFQMne9rXm0g==",
                    SecurityStamp = "SVKHSIPWR4JE76RDYMGA7MQGFIIBR3TX",
                    ConcurrencyStamp = "ce435a30-8d16-4770-a7fb-118fab13767b",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }, new ApplicationUser()
                {
                    Id = "5c141fdd-ab2f-49be-8a31-43aec63f918f",
                    PersonaId = Guid.Parse("f8d8b32a-7fad-43e1-a35e-e5a52621594d"),
                    UserName = "cpcoordinador@yopmail.com",
                    NormalizedUserName = "CPCOORDINADOR@YOPMAIL.COM",
                    Email = "CPCOORDINADOR@yopmail.com",
                    NormalizedEmail = "CPCOORDINADOR@YOPMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEDv98UwloGC1tI3Elt0TgyWk9DxWPi475t9/P2SOAO/TgHilR1/Tnp0kQpnctazOJw==",
                    SecurityStamp = "RZABU4JPNDQVJF4LWR5ZS755BS7H6HDR",
                    ConcurrencyStamp = "776e1d1a-0c3c-41b0-bb44-bcb151add354",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }, new ApplicationUser()
                {
                    Id = "3cbfb0a7-d323-4f84-9d14-daeef363f947",
                    PersonaId = Guid.Parse("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"),
                    UserName = "user3@gmail.com",
                    NormalizedUserName = "USER3@GMAIL.COM",
                    Email = "user3@gmail.com",
                    NormalizedEmail = "USER3@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEHRn5OJw1NKAwjB4w6dEsVjB2qi/bOR+8/WbmTjTa8lQCvTy6Xg4WqMtm6A/yYYxDw==",
                    SecurityStamp = "4LNTJIEMTON6KDXWASKMDXTFM2BCUCH6",
                    ConcurrencyStamp = "aa35d6da-54e6-476b-8810-4a182b8b07de",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
                , new ApplicationUser()
                {
                    Id = "cacd6063-c77d-437e-8cad-2e97ba1a0a6a",
                    PersonaId = Guid.Parse("8af22848-85fb-4c50-9d7d-1137fd84eb98"),
                    UserName = "user4@gmail.com",
                    NormalizedUserName = "USER4@GMAIL.COM",
                    Email = "user4@gmail.com",
                    NormalizedEmail = "USER4@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEGAsB2jMI6Q3cJTunTexm1lX3qqiVzHMV9sDws+mh3JcqAGY2og313y5uNjZ+32OAg==",
                    SecurityStamp = "WJR7XMGC332UT234BG7O35M6MIIGKQ2Q",
                    ConcurrencyStamp = "e3330e49-ac28-4952-a50a-a30e8222aed7",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                });
            }
            );

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }

    }
}
