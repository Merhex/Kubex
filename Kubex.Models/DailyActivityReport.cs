using System;
using System.Collections.Generic;

namespace Kubex.Models
{
    public class DailyActivityReport
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
    }
}