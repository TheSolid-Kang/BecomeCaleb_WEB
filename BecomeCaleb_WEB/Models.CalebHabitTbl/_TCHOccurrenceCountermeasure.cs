using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class _TCHOccurrenceCountermeasure
    {
        [Key]
        public int OccurrenceSeq { get; set; }
        [Key]
        public int CountermeasureSeq { get; set; }
        public bool IsSuccessful { get; set; }
        public int? EffectivenessRating { get; set; }
        public int? TimeToEffect { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [ForeignKey(nameof(CountermeasureSeq))]
        [InverseProperty(nameof(_TCHCountermeasure._TCHOccurrenceCountermeasures))]
        public virtual _TCHCountermeasure CountermeasureSeqNavigation { get; set; } = null!;
        [ForeignKey(nameof(OccurrenceSeq))]
        [InverseProperty(nameof(_TCHOccurrenceRecord._TCHOccurrenceCountermeasures))]
        public virtual _TCHOccurrenceRecord OccurrenceSeqNavigation { get; set; } = null!;
    }
}
