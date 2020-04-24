using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kubex.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IAddressRepository _addressRepository;

        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IUserRepository repository,
            IConfiguration configuration,
            SignInManager<User> signInManager,
            IAddressRepository addressRepository)
        {
            _repository = repository;
            _configuration = configuration;
            _signInManager = signInManager;
            _addressRepository = addressRepository;
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
                throw new ApplicationException(check.error);

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
                throw new ApplicationException(check.error);

            var result = await _userManager.RemoveFromRolesAsync(check.user, dto.Roles);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(check.user);

                return userToReturn;
            }

            throw new ApplicationException("Something went wrong trying to remove the given roles, please try again.");
        }

        public async Task<IEnumerable<UserToReturnDTO>> GetUsersAsync(ClaimsPrincipal requestingUser)
        {
            var users = new List<User>();

            foreach (var claim in requestingUser.Claims)
            {
                if (claim.Type == ClaimTypes.Role) 
                {
                    var usersInRole =  await _userManager.GetUsersInRoleAsync(claim.Value);
                    users.AddRange(usersInRole);
                }
            }

            var usersToReturn = _mapper.Map<IEnumerable<UserToReturnDTO>>(users.ToHashSet());

            return usersToReturn;
        }

        public async Task<string> GenerateJWTToken(UserLoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(double.Parse(_configuration.GetSection("AppSettings:TokenExperiryInSeconds").Value)),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserToReturnDTO> Register(UserRegisterDTO dto)
        {
            var newUser = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(newUser, dto.Password);
                   
            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(newUser);

                return userToReturn;
            }

            throw new ApplicationException($"Something went wrong trying to register the user: {result.Errors.FirstOrDefault().Description}");
        }

        public async Task<UserToReturnDTO> Login(UserLoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                throw new ApplicationException("We could not find an account with that given username and password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(user);

                return userToReturn;
            }

            throw new ApplicationException("We could not find an account with that given username and password.");
        }

        private async Task<(bool isValid, string error, User user)> ValidateModifyRolesDTO(ModifyRolesDTO dto) 
        {
            if (dto == null)
                return (false, "The data sent was invalid, please check the formatting or contact an administrator if you think this is an error.", null);

            if (dto.Name == null)
                return (false, "The name field in the data sent was empty.", null);
                
            var user = await _userManager.FindByNameAsync(dto.Name);

            if (user == null)
                return (false, "Could not find a user with the given username", null);

            foreach (var role in dto.Roles)
            {
                if (! await _roleManager.RoleExistsAsync(role))
                    return (false, $"The given role: {role}, does not exist.", null);
                
                if (! dto.RequestingUser.IsInRole(role))
                    return (false, $"You are not allowed to modify user {dto.Name}, to the given role: {role}.", null);
            }

            return (true, null, user);
        }
    }
}
