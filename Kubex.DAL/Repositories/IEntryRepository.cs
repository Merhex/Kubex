using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.DAL.Repositories
{
    public interface IEntryRepository : IRepository<Entry, int>
    {
        Entry Create(CreatingEntryDTO dto);
    }
}