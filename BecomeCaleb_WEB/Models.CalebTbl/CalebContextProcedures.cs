﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using BecomeCaleb_WEB.Models.CalebTbl;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace BecomeCaleb_WEB.Models.CalebTbl
{
    public partial class CalebContext
    {
        private ICalebContextProcedures _procedures;

        public virtual ICalebContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new CalebContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public ICalebContextProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
        }
    }

    public partial class CalebContextProcedures : ICalebContextProcedures
    {
        private readonly CalebContext _context;

        public CalebContextProcedures(CalebContext context)
        {
            _context = context;
        }
    }
}
