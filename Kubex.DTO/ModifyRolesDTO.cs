using System.Collections.Generic;

namespace Kubex.DTO
{
    public class ModifyRolesDTO
    {
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}