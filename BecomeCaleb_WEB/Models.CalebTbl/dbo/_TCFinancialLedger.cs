using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.CalebTbl
{
    /// <summary>
    /// 가계부
    /// </summary>
    [Keyless]
    [Table("_TCFinancialLedger")]
    [Index(nameof(InDate), Name = "_TCMinor_NIndex")]
    public partial class _TCFinancialLedger
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 가계부 내부코드
        /// </summary>
        public int FinancialSeq { get; set; }

        /// <summary>
        /// 생성일시
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? InDate { get; set; }

        /// <summary>
        /// 내역
        /// </summary>
        [StringLength(512)]
        public string? History { get; set; }

        /// <summary>
        /// 금액
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// _TCMajor Major 코드
        /// </summary>
        public int CategoryMaj { get; set; }

        /// <summary>
        /// _TCMinor Minor 코드
        /// </summary>
        public int CategoryMir { get; set; }

        /// <summary>
        /// 최종작업자
        /// </summary>
        public int? LastUserSeq { get; set; }

        /// <summary>
        /// 최종작업일시
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }
    }
}

