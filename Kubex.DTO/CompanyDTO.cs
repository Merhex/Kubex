using System.Collections.Generic;

namespace Kubex.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Email { get; set; }
        public string CustomerNumber { get; set; }
        public AddressDTO Address { get; set; }
        public IEnumerable<PostDTO> Posts { get; set; }
        public IEnumerable<ContactDTO> Contacts { get; set; }
    }
}