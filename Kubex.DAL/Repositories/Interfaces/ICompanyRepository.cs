using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepository<Company, int>
    {
         Task<Company> FindByNameAsync(string name);
    }
}