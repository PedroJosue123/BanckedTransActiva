using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TransActiva.Models;

namespace TransActiva.Context;

public partial class TransactivaDbContext : DbContext
{
    public TransactivaDbContext(DbContextOptions<TransactivaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<order> orders { get; set; }

    public virtual DbSet<user> users { get; set; }

    public virtual DbSet<userprofile> userprofiles { get; set; }

    public virtual DbSet<usertype> usertypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.Product).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pendiente'");
            entity.Property(e => e.Supplier).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.UserTypeId, "UserTypeId");

            entity.Property(e => e.UserId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FailedLoginAttempts)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
            entity.Property(e => e.LockoutUntil).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UserTypeId).HasColumnType("int(11)");

            entity.HasOne(d => d.UserType).WithMany(p => p.users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<userprofile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("PRIMARY");

            entity.HasIndex(e => e.ManagerEmail, "EmailManager").IsUnique();

            entity.HasIndex(e => e.ManagerDni, "ManagerDni").IsUnique();

            entity.HasIndex(e => e.Ruc, "Ruc").IsUnique();

            entity.HasIndex(e => e.UserId, "UserId").IsUnique();

            entity.Property(e => e.UserProfileId).HasColumnType("int(11)");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ManagerDni).HasMaxLength(8);
            entity.Property(e => e.ManagerEmail).HasMaxLength(150);
            entity.Property(e => e.ManagerName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Ruc).HasMaxLength(11);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithOne(p => p.userprofile)
                .HasForeignKey<userprofile>(d => d.UserId)
                .HasConstraintName("userprofiles_ibfk_1");
        });

        modelBuilder.Entity<usertype>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PRIMARY");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.UserTypeId).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
