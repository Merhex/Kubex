using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services;
using Kubex.DTO;
using Kubex.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;

        public UsersController(
            RoleManager<IdentityRole> roleManager,
            IUserService userService)
        {
            _roleManager = roleManager;
            _userService = userService;
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
            var user = await _userService.AddRoleToUserAsync(dto);

            return Ok(user);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/delete")]
        public async Task<IActionResult> DeleteRole(string userName, ModifyRolesDTO dto)
        {
            var user = await _userService.RemoveRoleFromUserAsync(dto);

            return Ok(user);
        }
    }
}