using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kubex.BLL.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public ContactService(
            IContactRepository contactRepository,
            ICompanyRepository companyRepository,
            UserManager<User> userService,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _userManager = userService;
        }

        public async Task<ContactDTO> CreateContactAsync(ContactDTO dto)
        {
            var contact = _mapper.Map<Contact>(dto);

            _contactRepository.Add(contact);

            if (await _contactRepository.SaveAll())
            {
                if (!string.IsNullOrWhiteSpace(dto.UserName)) 
                {
                    var user = await _userManager
                        .Users
                        .Include(x => x.Contacts)
                        .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

                    user.Contacts.Add(contact);

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                        return _mapper.Map<ContactDTO>(contact);
                    else
                        throw new ApplicationException("Something went wrong adding the contact to the user");

                }

                return _mapper.Map<ContactDTO>(contact);
            }
            
            throw new ApplicationException("Something went wrong adding a new contact.");
        }

        public Task DeleteAllContactsForCompany(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await FindContact(contactId);
            
            _contactRepository.Remove(contact);

            if (! await _contactRepository.SaveAll())
                throw new ApplicationException("Something went wrong deleting the contact.");
        }

        public Task<ContactDTO> GetContactAsync(int contactId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ContactDTO>> GetContactsForCompany(int companyId)
        {
            var company = await _companyRepository.Find(companyId);

            if (company == null)
                throw new ApplicationException("There is no company found with the given id.");

            var contacts = _mapper.Map<IEnumerable<ContactDTO>>(company.Contacts);

            return contacts;
        }
        public async Task<IEnumerable<ContactDTO>> GetContactsForUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                throw new ApplicationException("The is no user found with the given username.");

            var contacts = _mapper.Map<IEnumerable<ContactDTO>>(user.Contacts);

            return contacts;
        }

        public async Task UpdateContactAsync(ContactDTO dto)
        {
            var contact = await FindContact(dto.Id);

            contact.Type = dto.Type;
            contact.Value = dto.Value;

            if (! await _companyRepository.SaveAll())
                throw new ApplicationException("Sonething went wrong updating the contact>");
        }

        private async Task<Contact> FindContact(int id) => 
            await _contactRepository.Find(id) 
            ?? 
            throw new ArgumentNullException(null, "Could not find a contact with the given id.");
    }
}