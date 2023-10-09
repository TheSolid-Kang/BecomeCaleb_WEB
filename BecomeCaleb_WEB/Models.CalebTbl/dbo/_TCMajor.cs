using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;


namespace BecomeCaleb_WEB.Models.CalebTbl
{
    /// <summary>
    /// 시스템 제공 기타코드 대분류
    /// </summary>
    [Table("_TCMajor")]
    public partial class _TCMajor
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        [Key]
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 대분류코드
        /// </summary>
        [Key]
        public int MajorSeq { get; set; }

        /// <summary>
        /// 대분류명
        /// </summary>
        [StringLength(100)]
        public string MajorName { get; set; } = null!;

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

