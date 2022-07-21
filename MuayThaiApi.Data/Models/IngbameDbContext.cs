using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MuayThaiApi.Data.Models
{
    public partial class IngbameDbContext : DbContext
    {
        public IngbameDbContext()
        {
        }

        public IngbameDbContext(DbContextOptions<IngbameDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Afiliado> Afiliados { get; set; }
        public virtual DbSet<ClasesTomada> ClasesTomadas { get; set; }
        public virtual DbSet<MetodosPago> MetodosPagos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                       .SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json")
                                       .Build();
                var connectionString = configuration.GetConnectionString("MuayThaiConn");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afiliado>(entity =>
            {
                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.NombreAfiliado)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Afiliados)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Afiliados_UserId");
            });

            modelBuilder.Entity<ClasesTomada>(entity =>
            {
                entity.HasKey(e => e.ClaseId)
                    .HasName("PK_dbo_ClasesTomadas_Id");

                entity.Property(e => e.FechaClase).HasColumnType("datetime");

                entity.HasOne(d => d.Pago)
                    .WithMany(p => p.ClasesTomada)
                    .HasForeignKey(d => d.PagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_ClasesTomadas_PagoId");
            });

            modelBuilder.Entity<MetodosPago>(entity =>
            {
                entity.HasKey(e => e.MetodoId)
                    .HasName("PK_dbo_MetodosPago_Id");

                entity.ToTable("MetodosPago");

                entity.Property(e => e.MetodoDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.Property(e => e.EvidenciaUrl).IsUnicode(false);

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.HasOne(d => d.Afiliado)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.AfiliadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Pagos_AfiliadoId");

                entity.HasOne(d => d.Metodo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.MetodoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Pagos_MetodoId");
            });

            modelBuilder.Entity<PasswordsHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK_Sec_PasswordsHistory_Id");

                entity.ToTable("PasswordsHistory", "Sec");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PasswordsHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sec_PasswordsHistory_UserId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Sec");

                entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456D5E79EFC")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
