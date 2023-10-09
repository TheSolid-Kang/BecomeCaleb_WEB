using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models
{
    /// <summary>
    /// 미션
    /// </summary>
    [Table("_TCMission")]
    [Index(nameof(ChurchSeq), nameof(DiarySeq), Name = "TNPK_TCMission")]
    public partial class _TCMission
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 일기내부코드
        /// </summary>
        public int? DiarySeq { get; set; }

        /// <summary>
        /// 미션내부코드
        /// </summary>
        [Key]
        public int MissionSeq { get; set; }

        /// <summary>
        /// 미션제목
        /// </summary>
        [StringLength(512)]
        public string Title { get; set; }

        /// <summary>
        /// 미션Major-Minor코드
        /// </summary>
        public int? CategoryMMCode { get; set; }

        /// <summary>
        /// 미션결과
        /// </summary>
        public byte? Result { get; set; }

        /// <summary>
        /// 비고
        /// </summary>
        [StringLength(200)]
        public string Remark { get; set; }

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
