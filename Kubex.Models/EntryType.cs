using System.Collections.Generic;

namespace Kubex.Models
{
    public class EntryType
    {
        public byte Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}