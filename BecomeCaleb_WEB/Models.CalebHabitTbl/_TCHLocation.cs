using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BecomeCaleb_WEB.Models.CalebHabitTbl
{
    public partial class _TCHLocation
    {
        public _TCHLocation()
        {
            _TCHOccurrenceRecords = new HashSet<_TCHOccurrenceRecord>();
        }

        [Key]
        public int LocationSeq { get; set; }
        [StringLength(50)]
        public string LocationName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? LastUserSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDateTime { get; set; }

        [InverseProperty(nameof(_TCHOccurrenceRecord.LocationSeqNavigation))]
        public virtual ICollection<_TCHOccurrenceRecord> _TCHOccurrenceRecords { get; set; }
    }
}
