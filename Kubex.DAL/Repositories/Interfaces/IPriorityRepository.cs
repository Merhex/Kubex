using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IPriorityRepository : IRepository<Priority, byte>
    {
         Task<Priority> FindByLevel(string level);
    }
}