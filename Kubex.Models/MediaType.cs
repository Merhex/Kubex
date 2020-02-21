using System.Collections.Generic;

namespace Kubex.Models
{
    public class MediaType
    {
        public byte Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Media> Media { get; set; }
    }
}