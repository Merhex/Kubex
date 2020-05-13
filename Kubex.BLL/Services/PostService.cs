using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kubex.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;

        public PostService(IPostRepository postRepository,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IAddressRepository addressRepository)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _userManager = userManager;
            _postRepository = postRepository;
        }

        public async Task<PostDTO> CreatePostAsync(PostDTO dto)
        {
            var post = _mapper.Map<Post>(dto);

            _postRepository.Add(post);

            if (await _postRepository.SaveAll()) 
            {
                var postToReturn = _mapper.Map<PostDTO>(post);
                return postToReturn;
            }

            throw new ApplicationException("Unable to create post.");
        }

        public async Task<PostDTO> SetPostRolesAsync(ModifyPostRolesDTO dto) 
        {
            var post = await FindPostAsync(dto.Id);

            if (post.Roles.Count != 0) 
            {
                post.Roles.Clear();

                if (! await _postRepository.SaveAll())
                    throw new ApplicationException("Something went wrong clearing the roles of the given post.");
            }

            var newRoles = _roleManager.Roles.Where(r => dto.Roles.Any(n => n == r.Name));
            foreach (var role in newRoles)
            {
                post.Roles.Add(new PostRole
                { 
                    PostId = post.Id, 
                    RoleId = role.Id 
                });
            }

            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong adding new roles to the post.");

            foreach(var user in post.Users) 
                await RefreshUserPostRolesAsync(user.UserId);

            var postToReturn = _mapper.Map<PostDTO>(post);
            
            return postToReturn;
        }

        public async Task<UserToReturnDTO> SetUserPostsAsync(UpdateUserPostsDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                throw new ArgumentNullException(null, "Could not find a user with the given username.");

            // Clear current post membership:
            var currentPosts = await GetUserPostsAsync(user.Id);
            foreach (var post in currentPosts)
            {
                post.Users
                    .Remove(post.Users
                    .FirstOrDefault(p => p.UserId == user.Id
                ));
            }
            
            if(currentPosts.Count() != 0)
                if (! await _postRepository.SaveAll())
                    throw new ApplicationException("Something went wrong clearing the posts from the given user.");
    
            // Add the user to the new posts:
            foreach (var postId in dto.PostIds)
            {
                var newPost = await FindPostAsync(postId);
                newPost.Users.Add(new UserPost 
                { 
                    UserId = user.Id, 
                    PostId = postId 
                });
            }

            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong clearing the posts from the given user.");
    
            return await RefreshUserPostRolesAsync(user.Id);
        }

        public async Task<UserToReturnDTO> RefreshUserPostRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException(null, "Could not find a user with the given id.");

            // Remove user from previous roles:
            var oldUserRoles = await _userManager.GetRolesAsync(user);

            if (oldUserRoles.Count > 0)
                await _userManager.RemoveFromRolesAsync(user, oldUserRoles);
    
            // Find the roles this user is entitled to from post membership:
            var newPostRoles = await GetUserPostRolesAsync(userId);
    
            // Get the role names:
            var allRoles = await _roleManager.Roles.ToListAsync();
            var addTheseRoles = allRoles.Where(r => newPostRoles.Any(p => p.RoleId == r.Id));
            var roleNames = addTheseRoles.Select(n => n.Name);
    
            // Add the user to the proper roles:
            var result = await _userManager.AddToRolesAsync(user, roleNames);

            if (! result.Succeeded)
                throw new ApplicationException(result.Errors.FirstOrDefault().Description);

            var userToReturn = _mapper.Map<UserToReturnDTO>(user);
    
            return userToReturn;
        }

        public async Task<IEnumerable<PostRole>> GetUserPostRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userPosts = await GetUserPostsAsync(user.Id);

            var userPostRoles = new List<PostRole>();
            foreach (var post in userPosts)
                userPostRoles.AddRange(post.Roles);
            
            return userPostRoles;
        }

        public async Task<IEnumerable<Post>> GetUserPostsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userPosts = await _postRepository.FindRange(p => p.Users.Any(u => u.UserId == user.Id));

            return userPosts;
        }

        public async Task<PostDTO> GetPostAsync(int id) 
        {
            var post = await FindPostAsync(id);
            
            var postToReturn = _mapper.Map<PostDTO>(post);

            return postToReturn;
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = await FindPostAsync(postId);
    
            var currentPostUsers = await GetPostUsersAsync(postId);

            post.Roles.Clear();
            post.Users.Clear();
            _postRepository.Remove(post);
    
            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong removing the post.");
    
            // Reset all the user roles:
            foreach (var user in currentPostUsers)
                await RefreshUserPostRolesAsync(user.Id);
        }

        private async Task<IEnumerable<User>> GetPostUsersAsync(int postId)
        {
            var post = await FindPostAsync(postId);

            var users = new List<User>();
            foreach (var u in post.Users)
            {
                var user = await _userManager.FindByIdAsync(u.UserId);

                if (user != null)
                    users.Add(user);
            }

            return users;
        }

        public async Task UpdatePostAsync(UpdatePostDTO dto)
        {
            var post = await FindPostAsync(dto.PostId);
            
            //Update address
            post.Address = _mapper.Map<Address>(dto.Address);

            //Update company
            post.Company = _mapper.Map<Company>(dto.Company);

            //Update location
            post.Location = _mapper.Map<Location>(dto.Location);
            
            //Find all users
            //Add the to userposts from post.Users
            foreach(var username in dto.UserNames) 
            {
                var updateUserPostsDto = new UpdateUserPostsDTO { UserName = username, PostIds = new int[] { post.Id } };
                await SetUserPostsAsync(updateUserPostsDto);   
            }

            var modDto = new ModifyPostRolesDTO() { Id = dto.PostId, Roles = dto.Roles };
            await SetPostRolesAsync(modDto);
            
            _postRepository.Update(post);

            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Could not update the given post.");
        }

        public async Task<UserToReturnDTO> ClearUserFromPosts(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                throw new ApplicationException("Could not find a user with the given username.");
            
            var dto = new UpdateUserPostsDTO { UserName = userName, PostIds = new int[] {} };       
            await SetUserPostsAsync(dto);

            var userToReturn = _mapper.Map<UserToReturnDTO>(user);

            return userToReturn;
        }

        public async Task<IEnumerable<DailyActivityReportDTO>> GetDailyActivityReportsForPostAsync(int postId)
        {
            var post = await FindPostAsync(postId);

            var reports = _mapper.Map<IEnumerable<DailyActivityReportDTO>>(post.DailyActivityReports);
            return reports;
        }

        public async Task<DailyActivityReportDTO> GetDailyActivityReportFromPostAsync(int postId, int darId) 
        {
            var post = await FindPostAsync(postId);
            var dar = post.DailyActivityReports.FirstOrDefault(x => x.Id == darId);

            if (dar == null)
                throw new ArgumentNullException(null, "There is no Daily Activity Report found in this post with given Daily Activity Report id.");
            
            var darToReturn = _mapper.Map<DailyActivityReportDTO>(dar);
            return darToReturn;
        }

        private async Task<Post> FindPostAsync(int id) =>
            await _postRepository.Find(id)
            ?? throw new ArgumentNullException("Could not find a post with the given id.");
    }
}