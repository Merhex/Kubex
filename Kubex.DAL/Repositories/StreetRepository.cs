using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class StreetRepository 
        : Repository<Street, int>, IStreetRepository
    {
        public StreetRepository(DataContext context)
            : base(context)
        {
            
        }
    }
}