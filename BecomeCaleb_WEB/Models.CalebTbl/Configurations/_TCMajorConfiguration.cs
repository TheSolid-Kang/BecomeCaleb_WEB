﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using BecomeCaleb_WEB.Models.CalebTbl;
using BecomeCaleb_WEB.Models.CalebTbl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace BecomeCaleb_WEB.Models.CalebTbl.Configurations
{
    public partial class _TCMajorConfiguration : IEntityTypeConfiguration<_TCMajor>
    {
        public void Configure(EntityTypeBuilder<_TCMajor> entity)
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

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<_TCMajor> entity);
    }
}
