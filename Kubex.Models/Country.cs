using System.Collections.Generic;

namespace Kubex.Models
{
    public class Country
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}