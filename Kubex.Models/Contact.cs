using System.Collections.Generic;

namespace Kubex.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}