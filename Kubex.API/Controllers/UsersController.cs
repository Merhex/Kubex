using System;
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
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]        
        [HttpGet("{userName}", Name = "GetUser")]
        public async Task<IActionResult> Get(string userName)
        {
            try
            {
                var user = await _userService.GetUserAsync(userName);

                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/add")]
        public async Task<IActionResult> AddRole(string userName,  [FromBody] ModifyRolesDTO dto)
        {
            try
            {
                var user = await _userService.AddRoleToUserAsync(dto);

                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost("{userName}/roles/delete")]
        public async Task<IActionResult> DeleteRole(string userName, [FromBody] ModifyRolesDTO dto)
        {
            try
            {
                var user = await _userService.RemoveRoleFromUserAsync(dto);

                return Ok(user); 
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}