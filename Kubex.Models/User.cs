using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kubex.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}