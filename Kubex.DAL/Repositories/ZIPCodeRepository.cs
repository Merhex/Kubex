using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class ZIPCodeRepository 
        : Repository<ZIP, int>, IZIPCodeRepository
    {
        public ZIPCodeRepository(DataContext context)
            : base(context)
        {
            
        }
    }
}