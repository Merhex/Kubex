using System.Collections.Generic;

namespace Kubex.Models
{
    public class Priority
    {
        public byte Id { get; set; }
        public string Level { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}