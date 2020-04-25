using System.Collections.Generic;

namespace Kubex.DTO
{
    public class UserToReturnDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDTO Address { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}