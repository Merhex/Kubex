using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kubex.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public int LicenseId { get; set; }
        public virtual License License { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}