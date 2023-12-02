using Microsoft.EntityFrameworkCore;
namespace BecomeCaleb_WEB.Models.CalebTbl
{
    public partial class CalebContext : DbContext
    {
        public virtual DbSet<_TCDiary> _TCDiaries { get; set; } = null!;
        public virtual DbSet<_TCMajor> _TCMajors { get; set; } = null!;
        public virtual DbSet<_TCMinor> _TCMinors { get; set; } = null!;
        public virtual DbSet<_TCMission> _TCMissions { get; set; } = null!;
        public virtual DbSet<_TCSearchRecord> _TCSearchRecords { get; set; } = null!;
        public virtual DbSet<_TCUser> _TCUsers { get; set; } = null!;
        public virtual DbSet<_TCFinancialLedger> _TCFinancialLedgers { get; set; } = null!;

        public CalebContext() { }//231009: default 생성자를 만들지 않으면 'Unable to resolve service for type 오류'가 발생한다.
        public CalebContext(DbContextOptions<CalebContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Caleb;User ID=sa;Password=qp06910691!;Trust Server Certificate=True;Command Timeout=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations._TCDiaryConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TCMajorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TCMinorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TCMissionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TCSearchRecordConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TCUserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations._TCFinancialLedgerConfiguration());

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingGeneratedFunctions(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

