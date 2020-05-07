using System.Collections.Generic;

namespace Kubex.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LogoUrl { get; set; }
        public string CustomerNumber { get; set; }

        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}