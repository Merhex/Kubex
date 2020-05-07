using System.Collections.Generic;
using System.Security.Claims;

namespace Kubex.DTO
{
    public class ModifyRolesDTO
    {
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public ClaimsPrincipal RequestingUser { get; set; }
    }
}