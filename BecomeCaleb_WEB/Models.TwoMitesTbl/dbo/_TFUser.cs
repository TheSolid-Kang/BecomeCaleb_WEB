﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;


namespace BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema
{
    /// <summary>
    /// 사용자마스터
    /// </summary>
    [Table("_TFUser")]
    public partial class _TFUser
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        [Key]
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 사용자코드
        /// </summary>
        [Key]
        public int UserSeq { get; set; }

        /// <summary>
        /// 계정
        /// </summary>
        [StringLength(200)]
        public string? ResidID { get; set; }

        /// <summary>
        /// 사용자명
        /// </summary>
        [StringLength(100)]
        public string? UserName { get; set; }

        /// <summary>
        /// 사용자번호
        /// </summary>
        [StringLength(20)]
        public string? Empid { get; set; }

        /// <summary>
        /// 관리자여부(1 == 관리자, 0 == 일반)
        /// </summary>
        [StringLength(1)]
        public string IsAdministrator { get; set; } = null!;

        /// <summary>
        /// 구원여부(1 == 성도, 0 == 일반)
        /// </summary>
        [StringLength(1)]
        public string IsSaved { get; set; } = null!;

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

