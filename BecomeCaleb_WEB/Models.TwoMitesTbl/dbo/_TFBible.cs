using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema
{
    [Table("_TFBible")]
    [Index(nameof(Testament), nameof(Chapter), nameof(Verse), Name = "_TFBible_NIndex")]
    public partial class _TFBible
    {

        /// <summary>
        /// 성경내부코드
        /// </summary>
        [Key]
        public int BibleSeq { get; set; }

        /// <summary>
        /// 성경약어
        /// </summary>
        [StringLength(8)]
        public string? Testament { get; set; }

        /// <summary>
        /// 장
        /// </summary>
        public int? Chapter { get; set; }

        /// <summary>
        /// 절
        /// </summary>
        public int? Verse { get; set; }

        /// <summary>
        /// 말씀
        /// </summary>
        [StringLength(2048)]
        public string? Descript { get; set; }
    }
}

