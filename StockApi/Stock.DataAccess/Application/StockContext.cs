using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stock.DataAccess.Application
{
    public partial class StockContext : DbContext
    {
        public StockContext()
        {
        }

        public StockContext(DbContextOptions<StockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MItem> MItems { get; set; } = null!;
        public virtual DbSet<MItemLocation> MItemLocations { get; set; } = null!;
        public virtual DbSet<MLocation> MLocations { get; set; } = null!;
        public virtual DbSet<MSupplier> MSuppliers { get; set; } = null!;
        public virtual DbSet<MSupplierItem> MSupplierItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MItem>(entity =>
            {
                entity.ToTable("mItem");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SellingPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<MItemLocation>(entity =>
            {
                entity.ToTable("mItemLocation");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(255);

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.MItemLocations)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK_mItemLocation_Item");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.MItemLocations)
                    .HasForeignKey(d => d.IdLocation)
                    .HasConstraintName("FK_mItemLocation_Location");
            });

            modelBuilder.Entity<MLocation>(entity =>
            {
                entity.ToTable("mLocation");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<MSupplier>(entity =>
            {
                entity.ToTable("mSupplier");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<MSupplierItem>(entity =>
            {
                entity.ToTable("mSupplierItem");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdItemNavigation)
                    .WithMany(p => p.MSupplierItems)
                    .HasForeignKey(d => d.IdItem)
                    .HasConstraintName("FK_mSupplierItem_Item");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.MSupplierItems)
                    .HasForeignKey(d => d.IdSupplier)
                    .HasConstraintName("FK_mSupplierItem_Supplier");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
