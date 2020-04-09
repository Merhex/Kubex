using Kubex.DAL.Repositories.Interfaces;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public class CountryRepository 
        : Repository<Country, byte>, ICountryRepository
    {
        public CountryRepository(DataContext context)
            : base (context)
        {
            
        }
        
    }
}