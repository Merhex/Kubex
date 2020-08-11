using System.Collections.Generic;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;

namespace Kubex.BLL.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<ContactDTO> CreateContactAsync(ContactDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAllContactsForCompany(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteContactAsync(int contactId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ContactDTO> GetContactAsync(int contactId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ContactDTO>> GetContactsForCompany(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateContactAsync(ContactDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}