using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    [Index(nameof(StateCategory), Name = "IX_TCHStates_Category")]
    public partial class _TCHState
    {
        public _TCHState()
        {
            _TCHOccurrenceStates = new HashSet<_TCHOccurrenceState>();
        }

        [Key]
        public int StateSeq { get; set; }
        [StringLength(50)]
        public string StateName { get; set; } = null!;
        [StringLength(20)]
        public string StateCategory { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [InverseProperty(nameof(_TCHOccurrenceState.StateSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceState> _TCHOccurrenceStates { get; set; }
    }
}
