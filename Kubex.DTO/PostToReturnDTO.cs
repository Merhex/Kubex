using System.Collections.Generic;
using Kubex.Models;

namespace Kubex.DTO
{
    public class PostToReturnDTO
    {
        public Post Post { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}