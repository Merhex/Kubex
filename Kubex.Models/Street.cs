using System.Collections.Generic;

namespace Kubex.Models
{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}