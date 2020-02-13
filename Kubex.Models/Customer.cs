using System.Collections.Generic;

namespace Kubex.Models
{
    public class Customer : Company
    {
        public string ContractNumber { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}