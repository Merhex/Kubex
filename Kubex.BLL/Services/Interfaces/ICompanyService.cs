using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface ICompanyService
    {
         Task<CompanyDTO> CreateCompanyAsync(CompanyDTO dto);
         Task<CompanyDTO> GetCompanyAsync(int companyId);
         Task UpdateCompanyAsync(CompanyDTO dto);
         Task DeleteCompanyAsync(int companyId);
    }
}