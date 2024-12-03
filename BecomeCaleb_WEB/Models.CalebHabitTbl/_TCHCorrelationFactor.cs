using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    [Index(nameof(StressLevel), Name = "IX_TCHCorrelationFactors_StressLevel")]
    public partial class _TCHCorrelationFactor
    {
        [Key]
        public int FactorSeq { get; set; }
        public int? OccurrenceSeq { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? SleepHours { get; set; }
        public int? StressLevel { get; set; }
        public int? ExerciseMinutes { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? AloneHours { get; set; }
        [StringLength(20)]
        public string? WeatherCondition { get; set; }
        public int? MoodBeforeIncident { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [ForeignKey(nameof(OccurrenceSeq))]
        [InverseProperty(nameof(_TCHOccurrenceRecord._TCHCorrelationFactors))]
        public virtual _TCHOccurrenceRecord? OccurrenceSeqNavigation { get; set; }
    }
}
