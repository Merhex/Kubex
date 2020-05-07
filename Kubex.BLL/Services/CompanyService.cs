using System;
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

        public async Task<CompanyDTO> CreateAsync(CompanyDTO dto)
        {
            var company = _mapper.Map<Company>(dto);

            _companyRepository.Add(company);

            if (await _companyRepository.SaveAll())
            {
                return dto;
            }

            throw new ApplicationException("Unable to create company.");
        }
    }
}