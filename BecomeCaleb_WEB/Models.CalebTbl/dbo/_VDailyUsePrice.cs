using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;


namespace BecomeCaleb_WEB.Models.CalebTbl
{
    [Keyless]
    public partial class _VDailyUsePrice
    {
        [Column(TypeName = "date")]
        public DateTime? InDate { get; set; }
        public int? TotalPrice { get; set; }
    }
}

