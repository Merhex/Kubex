using System.Collections.Generic;

namespace Kubex.DTO
{
    public class CompanyToReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public AddressDTO Address { get; set; }
        public IEnumerable<PostDTO> Posts { get; set; }
    }
}