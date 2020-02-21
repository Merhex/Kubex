using System;
using System.Collections.Generic;

namespace Kubex.Models
{
    public class LicenseType
    {
        public byte Id { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
    }
}