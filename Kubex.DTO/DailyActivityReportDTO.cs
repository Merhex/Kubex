using System;
using System.Collections.Generic;

namespace Kubex.DTO
{
    public class DailyActivityReportDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<EntryDTO> Entries { get; set; }
    }
}