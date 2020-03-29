using System.Collections.Generic;

namespace Kubex.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GPSData { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
    }
}