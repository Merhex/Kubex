using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class MediaTypeRepository
        : Repository<MediaType, byte>, IMediaTypeRepository
    {
        public MediaTypeRepository(DataContext context)
            : base(context) { }
        
        public async Task<MediaType> FindByType(string type)
        {
            var types = await FindRange(x => x.Type == type);
            return types.FirstOrDefault();
        }
    }
}