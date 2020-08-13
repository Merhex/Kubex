using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.DTO;

namespace Kubex.BLL.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactDTO> CreateContactAsync(ContactDTO dto);
        Task<ContactDTO> GetContactAsync(int contactId);
        Task<IEnumerable<ContactDTO>> GetContactsForCompany(int companyId);
        Task<IEnumerable<ContactDTO>> GetContactsForUser(int userId);
        Task UpdateContactAsync(ContactDTO dto);
        Task DeleteContactAsync(int contactId);
        Task DeleteAllContactsForCompany(int companyId);
    }
}