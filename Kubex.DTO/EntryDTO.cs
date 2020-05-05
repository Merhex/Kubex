using System.Collections.Generic;

namespace Kubex.DTO
{
    public class EntryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public EntryDTO ParentEntry { get; set; }
        public DailyActivityReportDTO DailyActivityReport { get; set; }
        public string EntryType { get; set; }
        public string Priority { get; set; }
        public LocationDTO Location { get; set; }

        public IEnumerable<EntryDTO> ChildEntries { get; set; }
        public IEnumerable<MediaDTO> Media { get; set; }
    }
}