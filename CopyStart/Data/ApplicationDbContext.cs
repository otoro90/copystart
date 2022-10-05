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
              
            });


            modelBuilder.Entity<TipoActivo>(b =>
            {
                
            });

            modelBuilder.Entity<MarcaActivo>(b =>
            {
               
            });


            modelBuilder.Entity<ModeloActivo>(b =>
            {
                
            });

            modelBuilder.Entity<Procedimiento>(b =>
            {
               
            });

            modelBuilder.Entity<ProcedimientoTipoServicio>(b =>
            {
               
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
                    Email = "cpadmin@yopmail.com",
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
