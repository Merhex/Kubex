namespace Kubex.DTO
{
    public class PostDTO
    {
        public string Name { get; set; }
        public CompanyDTO Company { get; set; }
        public AddressDTO Address { get; set; }
        public LocationDTO Location { get; set; }
    }
}