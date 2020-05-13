using System;
using System.Collections.Generic;

namespace Kubex.Models
{
    public class DailyActivityReport
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}