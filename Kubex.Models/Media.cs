namespace Kubex.Models
{
    public class Media
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public byte EntryTypeId { get; set; }
        public virtual EntryType EntryType { get; set; }
        public byte MediaTypeId { get; set; }
        public virtual MediaType MediaType { get; set; }
    }
}