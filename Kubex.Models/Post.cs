using System.Collections.Generic;

namespace Kubex.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}