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
       
        [HttpGet("{userName}", Name = "GetUser")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await _userService.GetUserAsync(userName);

            return Ok(user);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/add")]
        public async Task<IActionResult> AddRole(string userName, ModifyRolesDTO dto)
        {
            dto.RequestingUser = HttpContext.User;
            var user = await _userService.AddRoleToUserAsync(dto);

            return Ok(user);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/delete")]
        public async Task<IActionResult> DeleteRole(string userName, ModifyRolesDTO dto)
        {
            dto.RequestingUser = HttpContext.User;
            var user = await _userService.RemoveRoleFromUserAsync(dto);

            return Ok(user); 
        }
    }
}