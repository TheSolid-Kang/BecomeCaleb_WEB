﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;


namespace BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema
{
    [Keyless]
    [Table("_TestTbl２")]
    public partial class _TestTbl２
    {
        [Column(TypeName = "ntext")]
        public string? test1 { get; set; }
        public int? idx { get; set; }
    }
}

