namespace Kubex.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Value { get; set; } //TODO: Clarify
        public int Sequence { get; set; } //TODO: Clarify
        
        public byte ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
    }
}