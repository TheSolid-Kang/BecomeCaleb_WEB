using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models
{
    /// <summary>
    /// 사용자마스터
    /// </summary>
    [Keyless]
    [Table("_TCSearchRecord")]
    public partial class _TCSearchRecord
    {

        /// <summary>
        /// 교회내부코드
        /// </summary>
        public int ChurchSeq { get; set; }

        /// <summary>
        /// 검색어
        /// </summary>
        [StringLength(512)]
        public string SearchKeyword { get; set; }

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
