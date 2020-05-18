using System.Collections.Generic;

namespace Kubex.DTO
{
    public class UpdatePostDTO
    {
        public PostDTO Post { get; set; }
        public IEnumerable<string> UserNames { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}