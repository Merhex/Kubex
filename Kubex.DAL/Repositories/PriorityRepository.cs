using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class PriorityRepository
        : Repository<Priority, byte>, IPriorityRepository
    {
        public PriorityRepository(DataContext context)
            : base(context) { }


        public async Task<Priority> FindByLevel(string level)
        {
            var levels = await FindRange(x => x.Level == level);
            return levels.FirstOrDefault();
        }
    }
}