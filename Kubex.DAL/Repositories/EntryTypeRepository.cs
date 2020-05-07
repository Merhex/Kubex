using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class EntryTypeRepository
        : Repository<EntryType, byte>, IEntryTypeRepository
    {
        public EntryTypeRepository(DataContext context) 
            : base(context) { }

        public async Task<EntryType> FindByType(string type)
        {
            var types = await FindRange(x => x.Type == type);
            return types.FirstOrDefault();
        }
    }
}