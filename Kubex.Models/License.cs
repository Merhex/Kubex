using System;

namespace Kubex.Models
{
    public class License
    {
        public int Id { get; set; }
        
        public DateTime StartDate { get; set; }

        public byte LicenseTypeId { get; set; }
        public virtual LicenseType LicenseType { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}