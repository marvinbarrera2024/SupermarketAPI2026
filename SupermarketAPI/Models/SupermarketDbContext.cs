using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SupermarketAPI.Models;

public partial class SupermarketDbContext : DbContext
{
    public SupermarketDbContext()
    {
    }

    public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-9KSCT6V\\SQLEXPRESS; Database=SupermarketDb; User Id=sa; Password=321; Encrypt=False; TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD881388B5");

            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Products)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__UserId__276EDEB3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C92211A3C");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E49D0389AF").IsUnique();

            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserRole)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
