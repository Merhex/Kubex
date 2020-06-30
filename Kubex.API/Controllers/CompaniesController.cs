using System;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Authorize] 
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CompanyDTO dto)
        {
            var company = await _companyService.CreateCompanyAsync(dto);

            return Ok(company);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id) 
        {
            var company = await _companyService.GetCompanyAsync(id);

            return Ok(company);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCompany(CompanyDTO dto) 
        {
            await _companyService.UpdateCompanyAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id) 
        {
            await _companyService.DeleteCompanyAsync(id);

            return NoContent();
        }
    }
}