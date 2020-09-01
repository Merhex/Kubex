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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(
            ICompanyRepository companyRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyDTO dto)
        {
            var company = _mapper.Map<Company>(dto);

            _companyRepository.Add(company);

            if(await _companyRepository.SaveAll()) 
            {
                var companyToReturn = _mapper.Map<CompanyDTO>(company);
                return companyToReturn;
                }

            throw new ApplicationException("Unable to create company.");
        }

        public async Task<CompanyDTO> GetCompanyAsync(int companyId)
        {
            var company = await FindCompanyAsync(companyId);

            if (company == null)
                throw new ApplicationException("There is no company found with the given id.");

            var companyToReturn = _mapper.Map<CompanyDTO>(company);
            return companyToReturn;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompaniesAsync()
        {
            var companies = new List<Company>();
            companies = await _companyRepository.GetAllCompanies();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return companiesToReturn;
        }

        public async Task DeleteCompanyAsync(int companyId) 
        {
            var company = await FindCompanyAsync(companyId);

            _companyRepository.Remove(company);

            if (! await _companyRepository.SaveAll())
                throw new ApplicationException("Something went wrong deleting the company.");
        }

        public async Task UpdateCompanyAsync(CompanyDTO dto) 
        {
            var company = await FindCompanyAsync(dto.Id);
            var updatedCompany = _mapper.Map<Company>(dto);

            company.Name = updatedCompany.Name;
            company.Address = updatedCompany.Address;
            company.CustomerNumber = updatedCompany.CustomerNumber;
            company.LogoUrl = updatedCompany.LogoUrl;
            company.Email = updatedCompany.Email;

            if (! await _companyRepository.SaveAll())
                throw new ApplicationException("Something went wrong updating the company");
        }

        private async Task<Company> FindCompanyAsync(int companyId) => 
            await _companyRepository.Find(companyId) 
            ?? throw new ArgumentNullException(null, "Could not find a company with the given id.");
    }
}