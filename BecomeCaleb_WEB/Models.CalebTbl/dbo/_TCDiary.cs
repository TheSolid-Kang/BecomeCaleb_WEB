using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;


namespace BecomeCaleb_WEB.Models.CalebTbl
{
    /// <summary>
    /// 일기
    /// </summary>
    [Table("_TCDiary")]
    public partial class _TCDiary
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 일기내부코드
        /// </summary>
        [Key]
        public int DiarySeq { get; set; }

        /// <summary>
        /// 일자
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? InDate { get; set; }

        /// <summary>
        /// 일기제목
        /// </summary>
        [StringLength(256)]
        public string? Title { get; set; }

        /// <summary>
        /// 일기본문
        /// </summary>
        public string? Record { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        [StringLength(200)]
        public string? Remark { get; set; }

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

