using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class _TCHAction
    {
        public _TCHAction()
        {
            _TCHOccurrenceActions = new HashSet<_TCHOccurrenceAction>();
        }

        [Key]
        public int ActionSeq { get; set; }
        [StringLength(100)]
        public string ActionName { get; set; } = null!;
        [StringLength(20)]
        public string ActionCategory { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [InverseProperty(nameof(_TCHOccurrenceAction.ActionSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceAction> _TCHOccurrenceActions { get; set; }
    }
}
