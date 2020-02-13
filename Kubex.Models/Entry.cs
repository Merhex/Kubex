using System;
using System.Collections.Generic;

namespace Kubex.Models
{
    public class Entry
    {
        public int Id { get; set; }

        public DateTime OccuranceDate { get; set; }
        public string Description { get; set; }

        public int? ParentEntryId { get; set; }
        public virtual Entry ParentEntry { get; set; }
        public int DailyActivityReportId { get; set; }
        public virtual DailyActivityReport DailyActivityReport { get; set; }
        public byte EntryTypeId { get; set; }
        public virtual EntryType EntryType { get; set; }
        public byte PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<Entry> ChildEntries { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
    }
}