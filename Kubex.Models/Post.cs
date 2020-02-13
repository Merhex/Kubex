using System.Collections.Generic;

namespace Kubex.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}