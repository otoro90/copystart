using System;
using System.Collections.Generic;
using System.Text;
using CopyStart.Entities;
using CopyStart.Helpers;
using Microsoft.AspNetCore.Identity;
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
        public virtual DbSet<Documento> Certificacion { get; set; }
        public virtual DbSet<Diagnostico> Diagnostico { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Procedimiento> Procedimiento { get; set; }
        public virtual DbSet<ProcedimientoTipoServicio> ProcedimientoTipoServicio { get; set; }
        public virtual DbSet<Repuesto> Repuesto { get; set; }
        public virtual DbSet<RepuestoProcedimiento> RepuestoProcedimiento { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<ServicioProcedimiento> ServicioProcedimiento { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<Soporte> Soporte { get; set; }
        public virtual DbSet<TipoServicio> TipoServicio { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoActivo> TipoActivo { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
        public virtual DbSet<MarcaActivo> MarcaActvo { get; set; }
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

            modelBuilder.Entity<TipoDocumento>(b =>
            {
                b.HasData(new Entities.TipoDocumento()
                {
                    Id = new Guid("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    Nombre = "Cedula de Ciudadania",
                    Codigo = "CC",
                    Descripcion = "CC",
                    Estado = true
                });
            });

            modelBuilder.Entity<Ubicacion>(b =>
            {
                b.HasData(new Entities.Ubicacion()
                {
                    CodigoLugar = 50001,
                    Lugar = "Villavicencio"
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
                    NumeroDocumento = "1234567890",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = 50001

                }, new Persona()
                {
                    Id = new Guid("f8d8b32a-7fad-43e1-a35e-e5a52621594d"),
                    Nombres = "Coordinador",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1234567890",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = 50001

                }, new Persona()
                {
                    Id = new Guid("2481e6f4-7aeb-43bf-8dac-e57c1d1568ed"),
                    Nombres = "Tecnico",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1234567890",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = 50001

                }
                , new Persona()
                {
                    Id = new Guid("8af22848-85fb-4c50-9d7d-1137fd84eb98"),
                    Nombres = "Cliente",
                    Apellidos = "Por defecto",
                    TipoDocumentoId = Guid.Parse("324df0a1-337d-45c4-bf79-eb3a01e14273"),
                    NumeroDocumento = "1234567890",
                    Direccion = "Calle 40, #33-18",
                    Telefono = 3188743948,
                    UbicacionId = 50001

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
                    UserName = "user1@gmail.com",
                    NormalizedUserName = "USER1@GMAIL.COM",
                    Email = "user1@gmail.com",
                    NormalizedEmail = "USER1@GMAIL.COM",
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
                    UserName = "user2@gmail.com",
                    NormalizedUserName = "USER2@GMAIL.COM",
                    Email = "user2@gmail.com",
                    NormalizedEmail = "USER2@GMAIL.COM",
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
