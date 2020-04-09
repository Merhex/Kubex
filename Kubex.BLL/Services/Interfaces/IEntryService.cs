using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IEntryService
    {
         Task<Entry> CreateEntryInReport(CreatingEntryDTO dto);
    }
}