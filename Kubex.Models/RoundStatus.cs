using System.Collections.Generic;

namespace Kubex.Models
{
    public class RoundStatus
    {
        public byte Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }
    }
}