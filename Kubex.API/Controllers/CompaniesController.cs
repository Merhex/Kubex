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

        [HttpGet("{id}/reports")]
        public async Task<IActionResult> GetDailyActivityReportsForCompany(int id) 
        {
            var reports = await _companyService.GetDailyActivityReportsForCompanyAsync(id);

            return Ok(reports);
        }

        [HttpGet("{companyId}/reports/{darId}")]
        public async Task<IActionResult> GetDailyActivityReportFromCompany(int companyId, int darId) 
        {
            var report = await _companyService.GetDailyActivityReportFromCompanyAsync(companyId, darId);

            return Ok(report);
        }

        [HttpPut]
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