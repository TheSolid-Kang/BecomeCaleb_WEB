using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BecomeCaleb_WEB.Models.CalebTbl;


namespace BecomeCaleb_WEB.Models.CalebTbl
{
    [Keyless]
    public partial class _VMonthUsePrice
    {
        public int? YYYY { get; set; }
        public int? mm { get; set; }
        public int? MonthUsePrice { get; set; }
    }
}

