using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public interface IEntryService
    {
         Task<Entry> CreateEntryInReport(CreatingEntryDTO dto);
    }
}