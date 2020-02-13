using System.Collections.Generic;

namespace Kubex.Models
{
    public class Landmap
    {
        public int Id { get; set; }

        public string FloorplanUrl { get; set; }

        public virtual ICollection<Checkpoint> Checkpoints { get; set; }
    }
}