using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kubex.DTO;
using Kubex.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Kubex.API.Development
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
        {
            if (!userManager.Users.Any()) 
            {
                var roles = new List<IdentityRole> 
                {
                    new IdentityRole { Name = "Agent" },
                    new IdentityRole { Name = "Administrator" },
                    new IdentityRole { Name = "Moderator" },
                    new IdentityRole { Name = "Manager" },
                    new IdentityRole { Name = "User" },
                    new IdentityRole { Name = "Company" }
                };

                foreach (var role in roles)
                {
                    roleManager.CreateAsync(role).Wait();
                }

                var adminUser = new User 
                {
                    UserName = "Admin",
                };

                var result = userManager.CreateAsync(adminUser, "!1Password").Result;

                if (result.Succeeded)
                {
                    var admin = userManager.FindByNameAsync("Admin").Result;
                    userManager.AddToRolesAsync(admin, new [] {"Administrator", "Moderator", "Manager", "Agent", "Company", "User"}).Wait();
                }
            }
        }
    }
}