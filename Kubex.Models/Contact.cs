using System.Collections.Generic;

namespace Kubex.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Value { get; set; }
        public int Sequence { get; set; }
        
        public byte ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}