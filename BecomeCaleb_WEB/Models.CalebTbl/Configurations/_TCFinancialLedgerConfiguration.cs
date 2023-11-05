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
    public partial class _TCFinancialLedgerConfiguration : IEntityTypeConfiguration<_TCFinancialLedger>
    {
        public void Configure(EntityTypeBuilder<_TCFinancialLedger> entity)
        {
            entity.HasAnnotation("Relational:Comment", "가계부");

            entity.Property(e => e.CategoryMaj).HasComment("_TCMajor Major 코드");

            entity.Property(e => e.CategoryMir).HasComment("_TCMinor Minor 코드");

            entity.Property(e => e.ChurchSeq).HasComment("교회내부코드");

            entity.Property(e => e.FinancialSeq)
                .ValueGeneratedOnAdd()
                .HasComment("가계부 내부코드");

            entity.Property(e => e.History).HasComment("내역");

            entity.Property(e => e.InDate).HasComment("생성일시");

            entity.Property(e => e.LastDateTime).HasComment("최종작업일시");

            entity.Property(e => e.LastUserSeq).HasComment("최종작업자");

            entity.Property(e => e.Price).HasComment("금액");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<_TCFinancialLedger> entity);
    }
}