using System.Collections.Generic;

namespace Kubex.Models
{
    public class ZIP
    {
        public int Id { get; set; }
        public int Code { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}