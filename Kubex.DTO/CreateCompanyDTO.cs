using Kubex.Models;

namespace Kubex.DTO
{
    public class CreateCompanyDTO
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public Address Address { get; set; }
    }
}