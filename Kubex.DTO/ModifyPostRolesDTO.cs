using System.Collections.Generic;

namespace Kubex.DTO
{
    public class ModifyPostRolesDTO
    {
        public int Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}