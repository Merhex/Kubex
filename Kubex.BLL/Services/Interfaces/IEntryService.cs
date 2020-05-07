using System.Threading.Tasks;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IEntryService
    {
         Task<EntryDTO> CreateEntryAsync(EntryDTO dto);
    }
}