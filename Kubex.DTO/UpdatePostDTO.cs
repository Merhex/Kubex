using System.Collections.Generic;

namespace Kubex.DTO
{
    public class UpdatePostDTO
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public AddressDTO Address { get; set; }
        public LocationDTO Location { get; set; }
        public CompanyDTO Company { get; set; }
        public IEnumerable<string> UserNames { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}