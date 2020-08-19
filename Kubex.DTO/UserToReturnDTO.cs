using System.Collections.Generic;

namespace Kubex.DTO
{
    public class UserToReturnDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public AddressDTO Address { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<int> PostIds { get; set; }
    }
}