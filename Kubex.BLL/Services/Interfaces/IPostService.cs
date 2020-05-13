using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> CreatePostAsync(PostDTO dto);
        Task<PostDTO> SetPostRolesAsync(ModifyPostRolesDTO dto);
        Task<UserToReturnDTO> SetUserPostsAsync(UpdateUserPostsDTO dto);
        Task<UserToReturnDTO> RefreshUserPostRolesAsync(string userId);
        Task<IEnumerable<PostRole>> GetUserPostRolesAsync(string userName);
        Task<IEnumerable<Post>> GetUserPostsAsync(string userName);
        Task<PostDTO> GetPostAsync(int id);
        Task DeletePostAsync(int postId);
        Task UpdatePostAsync(UpdatePostDTO dto);
        Task<UserToReturnDTO> ClearUserFromPosts(string userName);
        Task<DailyActivityReportDTO> GetDailyActivityReportFromPostAsync(int postId, int darId);
        Task<IEnumerable<DailyActivityReportDTO>> GetDailyActivityReportsForPostAsync(int postId);
    }
}