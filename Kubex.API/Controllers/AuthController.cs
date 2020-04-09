using System;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            try
            {
                var newUser = await _userService.Register(dto);

                return CreatedAtRoute
                (
                    "GetUser",
                    new { controller = "Users", userName = newUser.UserName },
                    newUser
                );
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            try
            {
                var user = await _userService.Login(dto);

                return Ok(new
                {
                    token = await _userService.GenerateJWTToken(dto),
                    user
                });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}