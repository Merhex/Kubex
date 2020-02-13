using System;
using System.Collections.Generic;

namespace Kubex.Models
{
    public class Round
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan Duration { get; set; }

        public byte RoundStatusId { get; set; }
        public virtual RoundStatus RoundStatus { get; set; }
        public int LandmapId { get; set; }
        public virtual Landmap Landmap { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}