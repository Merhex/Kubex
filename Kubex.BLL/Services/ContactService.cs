using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;

namespace Kubex.BLL.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public ContactService(
            IContactRepository contactRepository,
            ICompanyRepository companyRepository,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
        }

        public async Task<ContactDTO> CreateContactAsync(ContactDTO dto)
        {
            var contact = _mapper.Map<Contact>(dto);

            _contactRepository.Add(contact);

            if (await _contactRepository.SaveAll())
                return dto;
            
            throw new ApplicationException("Something went wrong adding a new contact.");

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