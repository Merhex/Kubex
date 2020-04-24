using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IStreetRepository : IRepository<Street, int>
    {
         Task<Street> FindByNameAsync(string name);
    }
}