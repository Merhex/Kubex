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
            var user = await FindUserAsync(userName);

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
            await ValidateModifyRolesDTO(dto);

            var user = await FindUserAsync(dto.Name);

            var result = await _userManager.AddToRolesAsync(user, dto.Roles);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(user);

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
            await ValidateModifyRolesDTO(dto);

            var user = await FindUserAsync(dto.Name);

            var result = await _userManager.RemoveFromRolesAsync(user, dto.Roles);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(user);

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
            var user = await FindUserAsync(dto.UserName);

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

            var securityToken = _configuration.GetSection("AppSettings:Token").Value;
            if (securityToken == null)
                throw new ArgumentNullException("securityToken", "The security token is not set. Please do so using AppSettings:Token in appSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityToken));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expirery = _configuration.GetSection("AppSettings:TokenExpireryInSeconds").Value;
            if (expirery == null)
                throw new ArgumentNullException("expirery", "The JWT token expirery value is not set. Please do so using AppSettings:TokenExpireryInSeconds in appSettings");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(double.Parse(expirery)),
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
                await _userManager.AddToRoleAsync(newUser, "User");

                var userToReturn = _mapper.Map<UserToReturnDTO>(newUser);

                return userToReturn;
            }

            throw new ApplicationException($"Something went wrong trying to register the user: {result.Errors.FirstOrDefault().Description}");
        }

        public async Task<UserToReturnDTO> Login(UserLoginDTO dto)
        {
            var user = await FindUserAsync(dto.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDTO>(user);

                return userToReturn;
            }

            throw new ArgumentNullException(null, "We could not find an account with that given username and password.");
        }

        public async Task UpdateUserAsync(UserRegisterDTO dto) 
        {
            var user = await FindUserAsync(dto.UserName);

            _mapper.Map(dto, user);

            await _userManager.UpdateAsync(user);

            if (! string.IsNullOrWhiteSpace(dto.Password)) 
            {
                var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.Password);

                if (! result.Succeeded)
                    throw new ApplicationException($"Could not update the password. {result.Errors.FirstOrDefault().Description}");
            }
        }

        private async Task<bool> ValidateModifyRolesDTO(ModifyRolesDTO dto) 
        {
            if (dto == null)
                throw new ApplicationException("The data sent was invalid, please check the formatting or contact an administrator if you think this is an error.");

            if (dto.Name == null)
                throw new ApplicationException("The name field in the data sent was empty.");
                
            var user = await FindUserAsync(dto.Name);

            foreach (var role in dto.Roles)
            {
                if (! await _roleManager.RoleExistsAsync(role))
                    throw new ArgumentNullException(null, $"The given role: {role}, does not exist.");
                
                if (! dto.RequestingUser.IsInRole(role))
                    throw new ApplicationException($"You are not allowed to modify user {dto.Name}, to the given role: {role}.");
            }

            return true;
        }

        public async Task<User> FindUserAsync(string userName) => 
            await _userManager.FindByNameAsync(userName)
            ?? throw new ArgumentNullException("Could not find a user with the given username.");
    }
}
