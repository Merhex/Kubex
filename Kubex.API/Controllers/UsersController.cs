using System;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var requestingUser = HttpContext.User;
            var users = await _userService.GetUsersAsync(requestingUser);

            return Ok(users);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetUsersFromPost(int postId) 
        {
            var users = await _userService.GetAllUsersFromPost(postId);

            return Ok(users);
        }
       
        [HttpGet("{userName}", Name = "GetUser")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await _userService.GetUserAsync(userName);

            return Ok(user);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("roles/add")]
        public async Task<IActionResult> AddRole(string userName, ModifyRolesDTO dto)
        {
            dto.RequestingUser = HttpContext.User;
            var user = await _userService.AddRoleToUserAsync(dto);

            return Ok(user);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpDelete("roles/delete")]
        public async Task<IActionResult> DeleteRole(string userName, ModifyRolesDTO dto)
        {
            dto.RequestingUser = HttpContext.User;
            var user = await _userService.RemoveRoleFromUserAsync(dto);

            return Ok(user); 
        }

        [Authorize(Roles = "Administrator, Manager, User, Agent")]
        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUser(UserRegisterDTO dto) 
        {
            if (HttpContext.User.Identity.Name == dto.UserName
                && HttpContext.User.Identity.IsAuthenticated
                || HttpContext.User.IsInRole("Administrator")) 
            {
                await _userService.UpdateUserAsync(dto);

                return NoContent();
            }

            return Unauthorized();
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser(string userName) 
        {
            await _userService.DeleteUserAsync(userName);

            return NoContent();
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpDelete("post/{postId}/{userName}")]
        public async Task<IActionResult> DeleteUserFromPost(int postId, string userName)
        {
            await _userService.DeleteUserFromPostAsync(userName, postId);

            return NoContent();
        }
    }
}