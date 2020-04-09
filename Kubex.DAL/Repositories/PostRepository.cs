using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class PostRepository 
        : Repository<Post, int>, IPostRepository
    {
        public PostRepository(DataContext context)
            : base(context)
        {
            
        }
    }
}