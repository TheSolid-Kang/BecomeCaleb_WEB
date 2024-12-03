using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class _TCHCountermeasure
    {
        public _TCHCountermeasure()
        {
            _TCHOccurrenceCountermeasures = new HashSet<_TCHOccurrenceCountermeasure>();
        }

        [Key]
        public int CountermeasureSeq { get; set; }
        [StringLength(100)]
        public string CountermeasureName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [InverseProperty(nameof(_TCHOccurrenceCountermeasure.CountermeasureSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceCountermeasure> _TCHOccurrenceCountermeasures { get; set; }
    }
}
