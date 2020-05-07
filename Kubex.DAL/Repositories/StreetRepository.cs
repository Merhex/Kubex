using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
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

        public async Task<Street> FindByNameAsync(string name)
        {
            var street = await FindRange(x => x.Name == name);
            return street.FirstOrDefault();
        }
    }
}