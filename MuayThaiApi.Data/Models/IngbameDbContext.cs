using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MuayThaiApi.Data.Config;

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

        public virtual DbSet<AssignRoleMenu> AssignRoleMenus { get; set; }
        public virtual DbSet<ClasesTomada> ClasesTomadas { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<MetodosPago> MetodosPagos { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSettings.Instance.GetConectionStringFromMainProyect("MuayThaiConn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignRoleMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AssignRoleMenu", "App");

                entity.HasIndex(e => new { e.RolId, e.MenuItemId }, "UK_App_AssignRoleMenu_RolMenu")
                    .IsUnique();

                entity.HasOne(d => d.MenuItem)
                    .WithMany()
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_AssignRoleMenu_MenuItemId");

                entity.HasOne(d => d.Rol)
                    .WithMany()
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_AssignRoleMenu_RolId");
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

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItems", "App");

                entity.HasIndex(e => e.Title, "UQ__MenuItem__2CB664DCFA3B8919")
                    .IsUnique();

                entity.HasIndex(e => e.TargetPage, "UQ__MenuItem__3B967D445E3A4C58")
                    .IsUnique();

                entity.Property(e => e.IconSource)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TargetPage)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
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

                entity.HasOne(d => d.Metodo)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.MetodoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Pagos_MetodoId");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Pagos_AfiliadoId");
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

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Afiliados_UserId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RolId)
                    .HasName("PK_App_Roles_Id");

                entity.ToTable("Roles", "App");

                entity.HasIndex(e => e.RolDescription, "UQ__Roles__E0258FCB3E05141C")
                    .IsUnique();

                entity.Property(e => e.RolDescription)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Sec");

                entity.HasIndex(e => e.UserName, "UQ__Users__C9F284563271D837")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sec_Users_RolId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
