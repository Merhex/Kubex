using System.Collections.Generic;

namespace Kubex.Models
{
    public class ZIP
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}