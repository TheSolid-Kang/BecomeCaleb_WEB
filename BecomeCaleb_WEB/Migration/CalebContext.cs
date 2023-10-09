using System;
using System.Collections.Generic;
using BecomeCaleb_WEB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BecomeCaleb_WEB.Migration
{
    public partial class CalebContext : DbContext
    {
        public virtual DbSet<_TCDiary> _TCDiaries { get; set; }
        public virtual DbSet<_TCMajor> _TCMajors { get; set; }
        public virtual DbSet<_TCMinor> _TCMinors { get; set; }
        public virtual DbSet<_TCMission> _TCMissions { get; set; }
        public virtual DbSet<_TCSearchRecord> _TCSearchRecords { get; set; }
        public virtual DbSet<_TCUser> _TCUsers { get; set; }

        public CalebContext()
        {
        }

        public CalebContext(DbContextOptions<CalebContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Caleb;User ID=sa;Password=qp06910691!;Trust Server Certificate=True;Command Timeout=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_TCDiary>(entity =>
            {
                entity.HasKey(e => e.DiarySeq)
                    .HasName("PK___TCDiary__DC076EFD3A2E4167");

                entity.HasAnnotation("Relational:Comment", "일기");

                entity.Property(e => e.DiarySeq).HasComment("일기내부코드");

                entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

                entity.Property(e => e.InDate).HasComment("일자");

                entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

                entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

                entity.Property(e => e.Record).HasComment("일기본문");

                entity.Property(e => e.Remark).HasComment("비고");

                entity.Property(e => e.Title).HasComment("일기제목");
            });

            modelBuilder.Entity<_TCMajor>(entity =>
            {
                entity.HasKey(e => new { e.MajorSeq, e.ChurchSeq })
                    .HasName("TPK_TCMajor");

                entity.HasAnnotation("Relational:Comment", "시스템 제공 기타코드 대분류");

                entity.Property(e => e.MajorSeq)
                    .ValueGeneratedOnAdd()
                    .HasComment("대분류코드");

                entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

                entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

                entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

                entity.Property(e => e.MajorName).HasComment("대분류명");

                entity.Property(e => e.Remark).HasComment("비고");
            });

            modelBuilder.Entity<_TCMinor>(entity =>
            {
                entity.HasAnnotation("Relational:Comment", "시스템 제공 기타코드 소분류");

                entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

                entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

                entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

                entity.Property(e => e.MajorSeq).HasComment("대분류코드");

                entity.Property(e => e.MinorName).HasComment("소분류명");

                entity.Property(e => e.MinorSeq).HasComment("소분류코드");

                entity.Property(e => e.Remark).HasComment("비고");
            });

            modelBuilder.Entity<_TCMission>(entity =>
            {
                entity.HasKey(e => e.MissionSeq)
                    .HasName("PK___TCMissi__892269AD4B2DBD1A");

                entity.HasAnnotation("Relational:Comment", "미션");

                entity.Property(e => e.MissionSeq).HasComment("미션내부코드");

                entity.Property(e => e.CategoryMMCode).HasComment("미션Major-Minor코드");

                entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

                entity.Property(e => e.DiarySeq).HasComment("일기내부코드");

                entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

                entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

                entity.Property(e => e.Remark).HasComment("비고");

                entity.Property(e => e.Result).HasComment("미션결과");

                entity.Property(e => e.Title).HasComment("미션제목");
            });

            modelBuilder.Entity<_TCSearchRecord>(entity =>
            {
                entity.HasAnnotation("Relational:Comment", "사용자마스터");

                entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

                entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

                entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

                entity.Property(e => e.SearchKeyword).HasComment("검색어");
            });

            modelBuilder.Entity<_TCUser>(entity =>
            {
                entity.HasKey(e => new { e.ChurchSeq, e.UserSeq })
                    .HasName("TPK_TMEmp");

                entity.HasAnnotation("Relational:Comment", "사용자마스터");

                entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

                entity.Property(e => e.UserSeq)
                    .ValueGeneratedOnAdd()
                    .HasComment("사용자코드");

                entity.Property(e => e.Empid).HasComment("사용자번호");

                entity.Property(e => e.IsAdministrator)
                    .IsFixedLength()
                    .HasComment("관리자여부(1 == 관리자, 0 == 일반)");

                entity.Property(e => e.IsSaved)
                    .IsFixedLength()
                    .HasComment("구원여부(1 == 성도, 0 == 일반)");

                entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

                entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

                entity.Property(e => e.ResidID).HasComment("계정");

                entity.Property(e => e.UserName).HasComment("사용자명");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
