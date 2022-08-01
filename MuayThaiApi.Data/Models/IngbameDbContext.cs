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
        public virtual DbSet<ClassAttendance> ClassAttendances { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<PasswordsHistory> PasswordsHistories { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Person> People { get; set; }
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

                entity.HasIndex(e => new { e.RoleId, e.MenuItemId }, "UK_App_AssignRoleMenu_RolMenu")
                    .IsUnique();

                entity.HasOne(d => d.MenuItem)
                    .WithMany()
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_AssignRoleMenu_MenuItemId");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_App_AssignRoleMenu_RolId");
            });

            modelBuilder.Entity<ClassAttendance>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PK_dbo_ClassAttendance_Id");

                entity.ToTable("ClassAttendance");

                entity.Property(e => e.AttendanceDate).HasColumnType("datetime");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.ClassAttendances)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_ClassAttendance_PaymentId");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("MenuItems", "App");

                entity.HasIndex(e => e.Title, "UQ__MenuItem__2CB664DCA521BA0D")
                    .IsUnique();

                entity.HasIndex(e => e.TargetPage, "UQ__MenuItem__3B967D448B2F20B0")
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

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.EvidenciaUrl).IsUnicode(false);

                entity.Property(e => e.FechaPago).HasColumnType("datetime");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Payments_MethodId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Payments_PersonId");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.MethodId)
                    .HasName("PK_dbo_PaymentMethods_Id");

                entity.Property(e => e.MethodDesc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CellPhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoUrl).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_People_UserId");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "App");

                entity.HasIndex(e => e.RoleDescription, "UQ__Roles__A2DDC1C936FDD558")
                    .IsUnique();

                entity.Property(e => e.RoleDescription)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Sec");

                entity.HasIndex(e => e.UserName, "UQ__Users__C9F2845660F31BAF")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

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

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sec_Users_RolId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
