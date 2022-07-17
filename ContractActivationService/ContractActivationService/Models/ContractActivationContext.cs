using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContractActivationService.Models
{
    public partial class ContractActivationContext : DbContext
    {
        public ContractActivationContext()
        {
        }

        public ContractActivationContext(DbContextOptions<ContractActivationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cont> Conts { get; set; } = null!;
        public virtual DbSet<ContAset> ContAsets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=Default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cont>(entity =>
            {
                entity.ToTable("CONT");

                entity.Property(e => e.ContId).HasColumnName("CONT_ID");

                entity.Property(e => e.ContEndDte)
                    .HasColumnType("datetime")
                    .HasColumnName("CONT_END_DTE");

                entity.Property(e => e.ContNumb)
                    .HasMaxLength(500)
                    .HasColumnName("CONT_NUMB");

                entity.Property(e => e.ContStrtDte)
                    .HasColumnType("datetime")
                    .HasColumnName("CONT_STRT_DTE");

                entity.Property(e => e.InsrBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("INSR_BY")
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.InsrDte)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("INSR_DTE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdtBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("UPDT_BY")
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.UpdtDte)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("UPDT_DTE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ContAset>(entity =>
            {
                entity.ToTable("CONT_ASET");

                entity.Property(e => e.ContAsetId).HasColumnName("CONT_ASET_ID");

                entity.Property(e => e.ContId).HasColumnName("CONT_ID");

                entity.Property(e => e.InsrBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("INSR_BY")
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.InsrDte)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("INSR_DTE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModlNme)
                    .HasMaxLength(500)
                    .HasColumnName("MODL_NME");

                entity.Property(e => e.UpdtBy)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("UPDT_BY")
                    .HasDefaultValueSql("(original_login())");

                entity.Property(e => e.UpdtDte)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("UPDT_DTE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VinNumb)
                    .HasMaxLength(500)
                    .HasColumnName("VIN_NUMB");

                entity.HasOne(d => d.Cont)
                    .WithMany(p => p.ContAsets)
                    .HasForeignKey(d => d.ContId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CONT_ASET__CONT___5CD6CB2B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
