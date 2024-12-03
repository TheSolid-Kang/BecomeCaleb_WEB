using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class CalebHabitContext : DbContext
    {
        public virtual DbSet<_TCHAction> _TCHActions { get; set; } = null!;
        public virtual DbSet<_TCHCorrelationFactor> _TCHCorrelationFactors { get; set; } = null!;
        public virtual DbSet<_TCHCountermeasure> _TCHCountermeasures { get; set; } = null!;
        public virtual DbSet<_TCHHabit> _TCHHabits { get; set; } = null!;
        public virtual DbSet<_TCHLocation> _TCHLocations { get; set; } = null!;
        public virtual DbSet<_TCHOccurrenceAction> _TCHOccurrenceActions { get; set; } = null!;
        public virtual DbSet<_TCHOccurrenceCountermeasure> _TCHOccurrenceCountermeasures { get; set; } = null!;
        public virtual DbSet<_TCHOccurrenceRecord> _TCHOccurrenceRecords { get; set; } = null!;
        public virtual DbSet<_TCHOccurrenceState> _TCHOccurrenceStates { get; set; } = null!;
        public virtual DbSet<_TCHState> _TCHStates { get; set; } = null!;

        public CalebHabitContext()
        {
        }

        public CalebHabitContext(DbContextOptions<CalebHabitContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Failover Partner=Caleb;Initial Catalog=Caleb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;Command Timeout=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_TCHAction>(entity =>
            {
                entity.HasKey(e => e.ActionSeq)
                    .HasName("PK___TCHActi__AA443E7E7FC93F97");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<_TCHCorrelationFactor>(entity =>
            {
                entity.HasKey(e => e.FactorSeq)
                    .HasName("PK___TCHCorr__272B49441F5E50C8");

                entity.HasOne(d => d.OccurrenceSeqNavigation)
                    .WithMany(p => p._TCHCorrelationFactors)
                    .HasForeignKey(d => d.OccurrenceSeq)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK___TCHCorre__Occur__5DB5E0CB");
            });

            modelBuilder.Entity<_TCHCountermeasure>(entity =>
            {
                entity.HasKey(e => e.CountermeasureSeq)
                    .HasName("PK___TCHCoun__58E3CC82DA4403F0");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<_TCHHabit>(entity =>
            {
                entity.HasKey(e => e.HabitSeq)
                    .HasName("PK___TCHHabi__E4D4C3F0C3ABB9B6");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<_TCHLocation>(entity =>
            {
                entity.HasKey(e => e.LocationSeq)
                    .HasName("PK___TCHLoca__EF334CC14DAF910B");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<_TCHOccurrenceAction>(entity =>
            {
                entity.HasKey(e => new { e.OccurrenceSeq, e.ActionSeq })
                    .HasName("PK___TCHOccu__77C61A9EF04FE107");

                entity.HasOne(d => d.ActionSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceActions)
                    .HasForeignKey(d => d.ActionSeq)
                    .HasConstraintName("FK___TCHOccur__Actio__53385258");

                entity.HasOne(d => d.OccurrenceSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceActions)
                    .HasForeignKey(d => d.OccurrenceSeq)
                    .HasConstraintName("FK___TCHOccur__Occur__52442E1F");
            });

            modelBuilder.Entity<_TCHOccurrenceCountermeasure>(entity =>
            {
                entity.HasKey(e => new { e.OccurrenceSeq, e.CountermeasureSeq })
                    .HasName("PK___TCHOccu__B8EC65B175B53B78");

                entity.HasOne(d => d.CountermeasureSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceCountermeasures)
                    .HasForeignKey(d => d.CountermeasureSeq)
                    .HasConstraintName("FK___TCHOccur__Count__58F12BAE");

                entity.HasOne(d => d.OccurrenceSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceCountermeasures)
                    .HasForeignKey(d => d.OccurrenceSeq)
                    .HasConstraintName("FK___TCHOccur__Occur__57FD0775");
            });

            modelBuilder.Entity<_TCHOccurrenceRecord>(entity =>
            {
                entity.HasKey(e => e.OccurrenceSeq)
                    .HasName("PK___TCHOccu__8D62597924A48D45");

                entity.HasIndex(e => e.OccurrenceDateTime, "IX_TCHOccurrenceRecords_DateTime")
                    .HasFilter("([IsDeleted]=(0))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.TimeOfDay).HasComputedColumnSql("(datepart(hour,[OccurrenceDateTime]))", true);

                entity.HasOne(d => d.HabitSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceRecords)
                    .HasForeignKey(d => d.HabitSeq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK___TCHOccur__Habit__3C54ED00");

                entity.HasOne(d => d.LocationSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceRecords)
                    .HasForeignKey(d => d.LocationSeq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK___TCHOccur__Locat__3D491139");
            });

            modelBuilder.Entity<_TCHOccurrenceState>(entity =>
            {
                entity.HasKey(e => new { e.OccurrenceSeq, e.StateSeq })
                    .HasName("PK___TCHOccu__7925275C7E2DD3B5");

                entity.HasOne(d => d.OccurrenceSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceStates)
                    .HasForeignKey(d => d.OccurrenceSeq)
                    .HasConstraintName("FK___TCHOccur__Occur__4E739D3B");

                entity.HasOne(d => d.StateSeqNavigation)
                    .WithMany(p => p._TCHOccurrenceStates)
                    .HasForeignKey(d => d.StateSeq)
                    .HasConstraintName("FK___TCHOccur__State__4F67C174");
            });

            modelBuilder.Entity<_TCHState>(entity =>
            {
                entity.HasKey(e => e.StateSeq)
                    .HasName("PK___TCHStat__4477E25E9EC98FB1");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
