using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface ICompanyService
    {
         Task<CompanyDTO> CreateAsync(CompanyDTO dto);
    }
}