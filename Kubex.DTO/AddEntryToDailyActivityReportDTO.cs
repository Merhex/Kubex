namespace Kubex.DTO
{
    public class AddEntryToDailyActivityReportDTO
    {
        public EntryDTO Entry { get; set; }
        public EntryDTO ParentEntry { get; set; }
        public DailyActivityReportDTO DailyActivityReport { get; set; }
    }
}