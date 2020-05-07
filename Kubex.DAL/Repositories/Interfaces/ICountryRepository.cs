using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface ICountryRepository : IRepository<Country, byte>
    {
         Task<Country> FindByNameAsync(string name);
    }
}