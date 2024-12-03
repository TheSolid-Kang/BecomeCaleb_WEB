using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class _TCHHabit
    {
        public _TCHHabit()
        {
            _TCHOccurrenceRecords = new HashSet<_TCHOccurrenceRecord>();
        }

        [Key]
        public int HabitSeq { get; set; }
        [StringLength(100)]
        public string HabitName { get; set; } = null!;
        [StringLength(500)]
        public string? HabitDesc { get; set; }
        [StringLength(20)]
        public string HabitCategory { get; set; } = null!;
        [StringLength(20)]
        public string? TargetFrequency { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [InverseProperty(nameof(_TCHOccurrenceRecord.HabitSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceRecord> _TCHOccurrenceRecords { get; set; }
    }
}
