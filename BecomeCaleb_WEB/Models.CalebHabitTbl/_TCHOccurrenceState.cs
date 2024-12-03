using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    [Index(nameof(Intensity), Name = "IX_TCHOccurrenceStates_Intensity")]
    [Index(nameof(StateSeq), Name = "IX_TCHOccurrenceStates_StateSeq")]
    public partial class _TCHOccurrenceState
    {
        [Key]
        public int OccurrenceSeq { get; set; }
        [Key]
        public int StateSeq { get; set; }
        public int? Intensity { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [ForeignKey(nameof(OccurrenceSeq))]
        [InverseProperty(nameof(_TCHOccurrenceRecord._TCHOccurrenceStates))]
        public virtual _TCHOccurrenceRecord OccurrenceSeqNavigation { get; set; } = null!;
        [ForeignKey(nameof(StateSeq))]
        [InverseProperty(nameof(_TCHState._TCHOccurrenceStates))]
        public virtual _TCHState StateSeqNavigation { get; set; } = null!;
    }
}
