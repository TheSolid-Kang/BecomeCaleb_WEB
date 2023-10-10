using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;


namespace BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema
{
    [Table("_TFAMajor")]
    public partial class _TFAMajor
    {
        [Key]
        public int MajorSeq { get; set; }
        [StringLength(100)]
        public string? MajorName { get; set; }
        [StringLength(200)]
        public string? Remark { get; set; }
        public int? LanguageSeq { get; set; }
        public int LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastDateTime { get; set; }
    }
}

