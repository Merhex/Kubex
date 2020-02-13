using System.Collections.Generic;

namespace Kubex.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }

        public string Identifier { get; set; } //TODO: Seperate table?
        public string Description { get; set; }
        public bool Mandatory { get; set; }

        public byte CheckpointTypeId { get; set; }
        public virtual CheckpointType CheckpointType { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}