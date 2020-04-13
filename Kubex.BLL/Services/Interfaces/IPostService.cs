using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostToReturnDTO> CreatePostAsync(CreatePostDTO dto);
        Task<PostToReturnDTO> SetPostRolesAsync(ModifyPostRolesDTO dto);
        Task<UserToReturnDTO> SetUserPostsAsync(UpdateUserPostsDTO dto);
        Task<UserToReturnDTO> RefreshUserPostRolesAsync(string userId);
        Task<IEnumerable<PostRole>> GetUserPostRolesAsync(string userName);
        Task<IEnumerable<Post>> GetUserPostsAsync(string userName);
        Task<PostToReturnDTO> GetPostAsync(int id);
        Task DeletePostAsync(int postId);
        Task<PostToReturnDTO> UpdatePostAsync(UpdatePostDTO dto);
        Task<UserToReturnDTO> ClearUserFromPosts(string userName);
    }
}