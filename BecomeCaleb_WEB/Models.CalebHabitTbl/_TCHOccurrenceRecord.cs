using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    [Index(nameof(HabitSeq), Name = "IX_TCHOccurrenceRecords_HabitSeq")]
    [Index(nameof(LocationSeq), Name = "IX_TCHOccurrenceRecords_LocationSeq")]
    [Index(nameof(TimeOfDay), Name = "IX_TCHOccurrenceRecords_TimeOfDay")]
    public partial class _TCHOccurrenceRecord
    {
        public _TCHOccurrenceRecord()
        {
            _TCHCorrelationFactors = new HashSet<_TCHCorrelationFactor>();
            _TCHOccurrenceActions = new HashSet<_TCHOccurrenceAction>();
            _TCHOccurrenceCountermeasures = new HashSet<_TCHOccurrenceCountermeasure>();
            _TCHOccurrenceStates = new HashSet<_TCHOccurrenceState>();
        }

        [Key]
        public int OccurrenceSeq { get; set; }
        public int HabitSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OccurrenceDateTime { get; set; }
        public int LocationSeq { get; set; }
        public int? CleanPeriodDays { get; set; }
        [StringLength(500)]
        public string? TriggerPattern { get; set; }
        public int? TimeOfDay { get; set; }
        public bool? IsDeleted { get; set; }
        [StringLength(255)]
        public string? Remark { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [ForeignKey(nameof(HabitSeq))]
        [InverseProperty(nameof(_TCHHabit._TCHOccurrenceRecords))]
        public virtual _TCHHabit HabitSeqNavigation { get; set; } = null!;
        [ForeignKey(nameof(LocationSeq))]
        [InverseProperty(nameof(_TCHLocation._TCHOccurrenceRecords))]
        public virtual _TCHLocation LocationSeqNavigation { get; set; } = null!;


        [InverseProperty(nameof(_TCHCorrelationFactor.OccurrenceSeqNavigation))]
        public virtual ICollection<_TCHCorrelationFactor> _TCHCorrelationFactors { get; set; }
        [InverseProperty(nameof(_TCHOccurrenceAction.OccurrenceSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceAction> _TCHOccurrenceActions { get; set; }
        [InverseProperty(nameof(_TCHOccurrenceCountermeasure.OccurrenceSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceCountermeasure> _TCHOccurrenceCountermeasures { get; set; }
        [InverseProperty(nameof(_TCHOccurrenceState.OccurrenceSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceState> _TCHOccurrenceStates { get; set; }
    }
}
