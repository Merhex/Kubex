using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class PostRepository 
        : Repository<Post, int>, IPostRepository
    {
        public PostRepository(DataContext context)
            : base(context) { }

        public async Task<Post> FindByNameAsync(string name) 
        {
            var posts = await FindRange(p => p.Name == name);
            return posts.SingleOrDefault();
        }
    }
}