using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public interface IUserService
    {
         Task<UserToReturnDTO> GetUserAsync(string userName);
         Task<UsersToReturnDTO> GetUsersAsync(Expression<Func<User, bool>> predicate);
         Task<UserToReturnDTO> AddRoleToUserAsync(ModifyRolesDTO dto);
         Task<UserToReturnDTO> AddRolesToUserAsync(ModifyRolesDTO dto);
         Task<UserToReturnDTO> RemoveRoleFromUserAsync(ModifyRolesDTO dto);
         Task<UserToReturnDTO> RemoveRolesFromUserAsync(ModifyRolesDTO dto);
         Task<UserToReturnDTO> Register(UserRegisterDTO dto);
         Task<UserToReturnDTO> Login(UserLoginDTO dto);
         Task<string> GenerateJWTToken(UserLoginDTO dto);
    }
}