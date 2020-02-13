using System;

namespace Kubex.Models
{
    public class Mission
    {
        public int CheckpointId { get; set; }
        public virtual Checkpoint Checkpoint { get; set; }
        public int RoundId { get; set; }
        public virtual Round Round { get; set; }

        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
     
        public byte MissionTypeId { get; set; }
        public virtual MissionType MissionType { get; set; }
    }
}