using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post, int>
    {
        Task<Post> FindByNameAsync(string name);
    }
}