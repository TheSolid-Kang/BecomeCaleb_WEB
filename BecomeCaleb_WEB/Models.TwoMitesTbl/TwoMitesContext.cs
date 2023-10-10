
using BecomeCaleb_WEB.Models.TwoMitesTbl.Configurations;
using BecomeCaleb_WEB.Models.TwoMitesTbl.dboSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
namespace BecomeCaleb_WEB.Models.TwoMitesTbl
{
    public partial class TwoMitesContext : DbContext
    {
        public virtual DbSet<_TFAMajor> _TFAMajors { get; set; } = null!;
        public virtual DbSet<_TFAMinor> _TFAMinors { get; set; } = null!;
        public virtual DbSet<_TFBible> _TFBibles { get; set; } = null!;
        public virtual DbSet<_TFMajor> _TFMajors { get; set; } = null!;
        public virtual DbSet<_TFMinor> _TFMinors { get; set; } = null!;
        public virtual DbSet<_TFUser> _TFUsers { get; set; } = null!;
        public virtual DbSet<_TestTbl> _TestTbls { get; set; } = null!;
        public virtual DbSet<_TestTbl２> _TestTbl２s { get; set; } = null!;
        public TwoMitesContext() { }
        public TwoMitesContext(DbContextOptions<TwoMitesContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TwoMites;User ID=sa;Password=qp06910691!;Trust Server Certificate=True;Command Timeout=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations._TFAMajorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TFAMinorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TFBibleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TFMajorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TFMinorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TFUserConfiguration());

        OnModelCreatingGeneratedProcedures(modelBuilder);
        OnModelCreatingGeneratedFunctions(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

