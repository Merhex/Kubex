using System.Threading.Tasks;
using Kubex.Models;

namespace Kubex.DAL.Repositories.Interfaces
{
    public interface IEntryTypeRepository : IRepository<EntryType, byte>
    {
         Task<EntryType> FindByType(string type);
    }
}