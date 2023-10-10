using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema
{
    /// <summary>
    /// 시스템 제공 기타코드 소분류
    /// </summary>
    [Keyless]
    [Table("_TFMinor")]
    [Index(nameof(MajorSeq), nameof(MinorSeq), Name = "_TFMinor_NIndex")]
    public partial class _TFMinor
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 대분류코드
        /// </summary>
        public int MajorSeq { get; set; }

        /// <summary>
        /// 소분류코드
        /// </summary>
        public int MinorSeq { get; set; }

        /// <summary>
        /// 소분류명
        /// </summary>
        [StringLength(200)]
        public string MinorName { get; set; } = null!;

        /// <summary>
        /// 비고
        /// </summary>
        [StringLength(200)]
        public string? Remark { get; set; }

        /// <summary>
        /// 최종작업자
        /// </summary>
        public int LastUserSeq { get; set; }

        /// <summary>
        /// 최종작업일시
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime LastDateTime { get; set; }
    }
}

