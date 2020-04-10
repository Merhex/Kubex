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

        public PostService(IPostRepository postRepository,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _postRepository = postRepository;
        }

        public async Task<Post> CreatePostAsync(CreatePostDTO dto)
        {
            var post = _mapper.Map<Post>(dto);

            _postRepository.Add(post);

            if (await _postRepository.SaveAll()) 
                return post;

            throw new ApplicationException("Unable to create post.");
        }

        public async Task<IdentityResult> SetPostRolesAsync(ModifyRolesDTO dto, IEnumerable<string> roles) 
        {
            var post = await _postRepository.FindByNameAsync(dto.Name) ??
                       await _postRepository.Find(Convert.ToInt32(dto.Name));
            
            post.Roles.Clear();

            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong clearing the roles of the given post.");

            var newRoles = _roleManager.Roles.Where(r => roles.Any(n => n == r.Name));
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
            {
                var result = await RefreshUserPostRolesAsync(user.UserId);

                if (! result.Succeeded)
                    throw new ApplicationException(result.Errors.FirstOrDefault().Description);
            }
            
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> SetUserPostsAsync(string userId, IEnumerable<int> postIds)
        {
            // Clear current post membership:
            var currentPosts = await GetUserPostsAsync(userId);
            foreach (var post in currentPosts)
            {
                post.Users
                    .Remove(post.Users
                    .FirstOrDefault(p => p.UserId == userId
                ));
            }
            
            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong clearing the posts from the given user.");
    
            // Add the user to the new posts:
            foreach (var postId in postIds)
            {
                var newPost = await _postRepository.Find(postId);
                newPost.Users.Add(new UserPost 
                { 
                    UserId = userId, 
                    PostId = postId 
                });
            }

            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong clearing the posts from the given user.");
    
            var result = await RefreshUserPostRolesAsync(userId);

            if (! result.Succeeded)
                throw new ApplicationException(result.Errors.FirstOrDefault().Description);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> RefreshUserPostRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ArgumentNullException("User");

            // Remove user from previous roles:
            var oldUserRoles = await _userManager.GetRolesAsync(user);

            if (oldUserRoles.Count > 0)
                await _userManager.RemoveFromRolesAsync(user, oldUserRoles);
    
            // Find the roles this user is entitled to from group membership:
            var newPostRoles = await GetUserPostRolesAsync(userId);
    
            // Get the damn role names:
            var allRoles = await _roleManager.Roles.ToListAsync();
            var addTheseRoles = allRoles.Where(r => newPostRoles.Any(p => p.RoleId == r.Id));
            var roleNames = addTheseRoles.Select(n => n.Name);
    
            // Add the user to the proper roles
            var result = await _userManager.AddToRolesAsync(user, roleNames);

            if (! result.Succeeded)
                throw new ApplicationException(result.Errors.FirstOrDefault().Description);
    
            return IdentityResult.Success;
        }

        public async Task<IEnumerable<PostRole>> GetUserPostRolesAsync(string userId)
        {
            var userPosts = await GetUserPostsAsync(userId);

            var userPostRoles = new List<PostRole>();
            foreach (var post in userPosts)
            {
                userPostRoles.AddRange(post.Roles);
            }
            return userPostRoles;
        }

        public async Task<IEnumerable<Post>> GetUserPostsAsync(string userId)
        {
            var userPosts = await _postRepository.FindRange(p => p.Users.Any(u => u.UserId == userId));

            return userPosts;
        }

        public async Task<IdentityResult> DeletePostAsync(int postId)
        {
            var post = await _postRepository.Find(postId);

            if (post == null)
                throw new ApplicationException("Could not find post with given id.");
    
            var currentPostUsers = (await GetPostUsersAsync(postId));

            post.Roles.Clear();
            post.Users.Clear();
            _postRepository.Remove(post);
    
            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Something went wrong removing the post.");
    
            // Reset all the user roles:
            foreach (var user in currentPostUsers)
            {
                var result = await this.RefreshUserPostRolesAsync(user.Id);

                if (! result.Succeeded)
                    throw new ApplicationException(result.Errors.FirstOrDefault().Description);
            }

            return IdentityResult.Success;
        }

        public async Task<IEnumerable<User>> GetPostUsersAsync(int postId)
        {
            var post = await _postRepository.Find(postId);

            var users = new List<User>();
            foreach (var u in post.Users)
            {
                var user = await _userManager.FindByIdAsync(u.UserId);
                users.Add(user);
            }

            return users;
        }

        public async Task<IdentityResult> UpdateGroupAsync(Post post)
        {
            _postRepository.Update(post);

            if (! await _postRepository.SaveAll())
                throw new ApplicationException("Could not update the given post.");

            foreach (var postUser in post.Users)
            {
                var result = await RefreshUserPostRolesAsync(postUser.UserId);

                if (! result.Succeeded)
                    throw new ApplicationException(result.Errors.FirstOrDefault().Description);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ClearUserFromPosts(string userId)
        {
            return await SetUserPostsAsync(userId, new int[] { });
        }
    }
}