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
    public partial class _VCategoryUsePriceConfiguration : IEntityTypeConfiguration<_VCategoryUsePrice>
    {
        public void Configure(EntityTypeBuilder<_VCategoryUsePrice> entity)
        {
            entity.ToView("_VCategoryUsePrice");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<_VCategoryUsePrice> entity);
    }
}
