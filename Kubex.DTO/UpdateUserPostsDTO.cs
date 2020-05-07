using System.Collections.Generic;

namespace Kubex.DTO
{
    public class UpdateUserPostsDTO
    {
        public string UserName { get; set; }
        public IEnumerable<int> PostIds { get; set; }
    }
}