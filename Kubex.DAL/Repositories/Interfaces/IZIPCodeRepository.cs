using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IZIPCodeRepository : IRepository<ZIP, int>
    {
         Task<ZIP> FindByCodeAsync(string code);
    }
}