using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ZIP> FindByCodeAsync(string code)
        {
            var zipCode = await FindRange(x => x.Code == code);
            return zipCode.FirstOrDefault();
        }
    }
}