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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin,ApplicationRoleClaim,ApplicationUserToken>
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
        public virtual DbSet<Ubicacion> Ubicacion{ get; set; }
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




           
            modelBuilder
                .Entity<Activo>()
                .Property(e => e.Estado)
                .HasConversion(
                    v => v.ToString(),
                    v => (EstadosActivo)Enum.Parse(typeof(EstadosActivo), v));

            modelBuilder
                .Entity<Solicitud>()
                .Property(e => e.Estado)
                .HasConversion(
                    v => v.ToString(),
                    v => (EstadosSolicitud)Enum.Parse(typeof(EstadosSolicitud), v));

            modelBuilder
                .Entity<Servicio>()
                .Property(e => e.Estado)
                .HasConversion(
                    v => v.ToString(),
                    v => (EstadosServicio)Enum.Parse(typeof(EstadosServicio), v));


            modelBuilder.Entity<ProcedimientoTipoServicio>()
          .HasIndex(p => new { p.TipoServicioId, p.ProcedimientoId })
            .IsUnique(true);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");
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
            });

            modelBuilder.Entity<ApplicationRoleClaim>(b =>
            {
                b.ToTable("RoleClaims");
            });

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.ToTable("UserRoles");
            });

            modelBuilder.Entity<Persona>(b =>
            {
                // Each Persona can have many Users
                b.HasMany(e => e.Users)
                    .WithOne(e => e.Persona)
                    .HasForeignKey(ul => ul.PersonaId);
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
            });

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
