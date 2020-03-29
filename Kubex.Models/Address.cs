using System.Collections.Generic;

namespace Kubex.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int HouseNumber { get; set; }
        public string AppartementBus { get; set; }

        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public int ZIPId { get; set; }
        public virtual ZIP ZIP { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}