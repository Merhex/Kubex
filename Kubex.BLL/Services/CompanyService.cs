using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kubex.BLL.Services.Interfaces;
using Kubex.DAL.Repositories.Interfaces;
using Kubex.DTO;
using Kubex.Models;
using Kubex.Models.Extensions;

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

            if (await _companyRepository.SaveAll())
                return dto;

            throw new ApplicationException("Unable to create company.");
        }

        public async Task<CompanyDTO> GetCompanyAsync(int companyId)
        {
            var company = await FindCompanyAsync(companyId);

            var companyToReturn = _mapper.Map<CompanyDTO>(company);
            return companyToReturn;
        }

        public async Task<IEnumerable<DailyActivityReportDTO>> GetDailyActivityReportsForCompanyAsync(int companyId)
        {
            var company = await FindCompanyAsync(companyId);

            var reports = _mapper.Map<IEnumerable<DailyActivityReportDTO>>(company.DailyActivityReports);
            return reports;
        }

        public async Task<DailyActivityReportDTO> GetDailyActivityReportFromCompanyAsync(int companyId, int darId) 
        {
            var company = await FindCompanyAsync(companyId);
            var dar = company.DailyActivityReports.FirstOrDefault(x => x.Id == darId);

            if (dar == null)
                throw new ArgumentNullException(null, "There is no Daily Activity Report found in this company with given Daily Activity Report id.");
            
            var darToReturn = _mapper.Map<DailyActivityReportDTO>(dar);
            return darToReturn;
        }

        public async Task DeleteCompanyAsync(int companyId) 
        {
            var company = await FindCompanyAsync(companyId);

            _companyRepository.Remove(company);

            if (! await _companyRepository.SaveAll())
                throw new ApplicationException("Something went wrong deleting the company.");
        }

        public async Task<CompanyDTO> UpdateCompanyAsync(CompanyDTO dto) 
        {
            var company = await FindCompanyAsync(dto.Id);
            var newCompany = _mapper.Map<Company>(dto);

            company.UpdateValues(newCompany);

            if (! await _companyRepository.SaveAll())
                throw new ApplicationException("Something went wrong updating the company");
            
            return dto;
        }

        private async Task<Company> FindCompanyAsync(int companyId) => 
            await _companyRepository.Find(companyId) 
            ?? throw new ArgumentNullException(null, "Could not find a company with the given id.");
    }
}