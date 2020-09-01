using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;

        public CompaniesController(
            ICompanyService companyService,
            IFileService fileService,
            IEmailService emailService)
        {
            _fileService = fileService;
            _emailService = emailService;
            _companyService = companyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CompanyDTO dto)
        {
            var company = await _companyService.CreateCompanyAsync(dto);

            return Ok(company);
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile file)
        {
            var fileLocation = await _fileService.UploadImage(file);

            return Ok(new { path = fileLocation });
        }

        [HttpPost("report/{companyId}")]
        public async Task<ActionResult> SendReports(int companyId) 
        {
            await _emailService.SendReport(companyId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();

            return Ok(companies);
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