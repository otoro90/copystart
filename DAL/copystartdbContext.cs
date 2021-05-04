using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace DAL
{
    public partial class copystartdbContext : DbContext
    {
        public copystartdbContext()
        {
        }

        public copystartdbContext(DbContextOptions<copystartdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SolicitudServicio> SolicitudesServicio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql("Server=www.tecnicoexperto.com;Port=5432;Database=copystartdb;User Id=postgres;Password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolicitudServicio>(entity =>
            {
                entity.ToTable("SolicitudesServicios","public");

                entity.HasKey(o => o.Id);

                entity.Property(e => e.SerialEquipo)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.NombreCliente)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CelularCliente)
                    .UsePropertyAccessMode(PropertyAccessMode.Field);

                entity.Property(e => e.DireccionCliente)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Descripcion)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FechaSolicitud)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
