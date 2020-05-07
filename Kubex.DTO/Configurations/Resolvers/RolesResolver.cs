using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.Models;
using Microsoft.AspNetCore.Identity;

namespace Kubex.DTO.Configurations.Resolvers
{
    public class RolesResolver : IValueResolver<User, UserToReturnDTO, IEnumerable<string>>
    {
        private readonly UserManager<User> _userManager;

        public RolesResolver(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<string> Resolve(User source, UserToReturnDTO destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            return Task.Run(async () =>
                await _userManager.GetRolesAsync(source)).Result;
        }
    }
}