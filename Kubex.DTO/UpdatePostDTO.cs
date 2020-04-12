using System.Collections.Generic;
using Kubex.Models;

namespace Kubex.DTO
{
    public class UpdatePostDTO
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public Location Location { get; set; }
        public Company Company { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}