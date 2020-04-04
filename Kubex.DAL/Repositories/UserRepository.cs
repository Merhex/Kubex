using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(DataContext context)
            : base(context)
        {
            
        }
    }
}