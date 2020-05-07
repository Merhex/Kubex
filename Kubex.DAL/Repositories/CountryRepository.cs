using System.Linq;
using System.Threading.Tasks;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class CountryRepository 
        : Repository<Country, byte>, ICountryRepository
    {
        public CountryRepository(DataContext context)
            : base (context) { }
        
        public async Task<Country> FindByNameAsync(string name) 
        {
            var country = await FindRange(x => x.Name == name);
            return country.FirstOrDefault();
        }
    }
}