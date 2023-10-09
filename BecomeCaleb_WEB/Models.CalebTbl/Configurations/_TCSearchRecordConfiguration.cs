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
    public partial class _TCSearchRecordConfiguration : IEntityTypeConfiguration<_TCSearchRecord>
    {
        public void Configure(EntityTypeBuilder<_TCSearchRecord> entity)
        {
            entity.HasAnnotation("Relational:Comment", "사용자마스터");

            entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

            entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

            entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

            entity.Property(e => e.SearchKeyword).HasComment("검색어");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<_TCSearchRecord> entity);
    }
}