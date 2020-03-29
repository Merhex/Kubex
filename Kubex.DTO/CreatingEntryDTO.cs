using System;
using System.Collections.Generic;
using Kubex.Models;

namespace Kubex.DTO
{
    public class CreatingEntryDTO
    {
        public string Description { get; set; }
        public Entry ParentEntry { get; set; }
        public DailyActivityReport DailyActivityReport { get; set; }
        public EntryType EntryType { get; set; }
        public Priority Priority { get; set; }
        public Location Location { get; set; }

        public virtual ICollection<Entry> ChildEntries { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}