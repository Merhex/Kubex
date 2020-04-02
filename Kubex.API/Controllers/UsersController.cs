using System.Threading.Tasks;
using AutoMapper;
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

        public UsersController(IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("{userName}", Name = "GetUser")]
        public async Task<IActionResult> Get(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return BadRequest("We could not find a user with the given username.");

            var userToReturn = _mapper.Map<UserToReturnDTO>(user);

            return Ok(userToReturn);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/add")]
        public async Task<IActionResult> AddRole(string userName, AddRoleDTO dto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return BadRequest("We could not find a user with the given username.");

            if (await _roleManager.FindByNameAsync(dto.Role) == null)
                return BadRequest("The given role does not exist.");

            await _userManager.AddToRoleAsync(user, dto.Role);

            return Ok();
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/delete")]
        public async Task<IActionResult> DeleteRole(AddRoleDTO dto, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return BadRequest("We could not find a user with the given username.");

            if (await _roleManager.FindByNameAsync(dto.Role) == null)
                return BadRequest("The given role does not exist.");

            await _userManager.RemoveFromRoleAsync(user, dto.Role);

            return Ok();
        }
    }
}