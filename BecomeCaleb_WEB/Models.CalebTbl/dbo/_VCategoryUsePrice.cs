using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;


namespace BecomeCaleb_WEB.Models.CalebTbl
{
    [Keyless]
    public partial class _VCategoryUsePrice
    {
        public int? YYYY { get; set; }
        public int? MM { get; set; }
        public int CategoryMir { get; set; }
        [StringLength(200)]
        public string? Category { get; set; }
        public int? TotPrice { get; set; }
    }
}

