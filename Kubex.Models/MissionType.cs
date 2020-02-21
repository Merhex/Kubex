using System.Collections.Generic;

namespace Kubex.Models
{
    public class MissionType
    {
        public byte Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}