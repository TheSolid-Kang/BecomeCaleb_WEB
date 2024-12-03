using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class _TCHOccurrenceAction
    {
        [Key]
        public int OccurrenceSeq { get; set; }
        [Key]
        public int ActionSeq { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [ForeignKey(nameof(ActionSeq))]
        [InverseProperty(nameof(_TCHAction._TCHOccurrenceActions))]
        public virtual _TCHAction ActionSeqNavigation { get; set; } = null!;
        [ForeignKey(nameof(OccurrenceSeq))]
        [InverseProperty(nameof(_TCHOccurrenceRecord._TCHOccurrenceActions))]
        public virtual _TCHOccurrenceRecord OccurrenceSeqNavigation { get; set; } = null!;
    }
}
