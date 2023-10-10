using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;


namespace BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema
{
    [Keyless]
    [Table("_TFAMinor")]
    public partial class _TFAMinor
    {
        public int MinorSeq { get; set; }
        public int MajorSeq { get; set; }
        [StringLength(100)]
        public string MinorName { get; set; } = null!;
        [StringLength(200)]
        public string? Remark { get; set; }
        public int? LanguageSeq { get; set; }
        public int LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastDateTime { get; set; }
    }
}

