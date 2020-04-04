using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.DAL.Repositories;
using Kubex.DTO;
using Kubex.Models;
using Microsoft.AspNetCore.Identity;

namespace Kubex.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _repository;

        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper, IUserRepository repository)
        {
            _repository = repository;
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        
        public async Task<UserToReturnDTO> GetUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                throw new ApplicationException("Could not find a user with the given username.");

            var userToReturn = _mapper.Map<UserToReturnDTO>(user);

            return userToReturn;
        }

        public async Task<UserToReturnDTO> AddRoleToUserAsync(ModifyRolesDTO dto)
        {
            var userToReturn = await AddRolesToUserAsync(dto);

            return userToReturn;
        }

        public async Task<UserToReturnDTO> AddRolesToUserAsync(ModifyRolesDTO dto)
        {
            var check = await ValidateModifyRolesDTO(dto);

            if (! check.isValid) 
            {
                throw new ApplicationException(check.error);
            }

            var result = await _userManager.AddToRolesAsync(check.user, dto.Roles);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(check.user);

                return userToReturn;
            }

            throw new ApplicationException("Something went wrong trying to add the given roles, please try again.");
        }

        public async Task<UserToReturnDTO> RemoveRoleFromUserAsync(ModifyRolesDTO dto) 
        {
            var userToReturn = await RemoveRolesFromUserAsync(dto);

            return userToReturn;
        }

        public async Task<UserToReturnDTO> RemoveRolesFromUserAsync(ModifyRolesDTO dto) 
        {
            var check = await ValidateModifyRolesDTO(dto);

            if (! check.isValid) 
            {
                throw new ApplicationException(check.error);
            }

            var result = await _userManager.RemoveFromRolesAsync(check.user, dto.Roles);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(check.user);

                return userToReturn;
            }

            throw new ApplicationException("Something went wrong trying to remove the given roles, please try again.");
        }

        public async Task<UsersToReturnDTO> GetUsersAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await _repository.FindRange(predicate).ToListAsync();

            var usersToReturn = _mapper.Map<UsersToReturnDTO>(users);

            return usersToReturn;
        }

        private async Task<(bool isValid, string error, User user)> ValidateModifyRolesDTO(ModifyRolesDTO dto) 
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                return (false, "Could not find a user with the given username", null);

            foreach (var role in dto.Roles)
            {
                if (! await _roleManager.RoleExistsAsync(role))
                    return (false, $"The given role: {role} does not exist.", null);
            }

            return (true, null, user);
        }
    }
}