using Kubex.Models;

namespace Kubex.DTO
{
    public class CreatePostDTO
    {
        public string Name { get; set; }
        public Company Company { get; set; }
        public Address Address { get; set; }
        public Location Location { get; set; }
    }
}