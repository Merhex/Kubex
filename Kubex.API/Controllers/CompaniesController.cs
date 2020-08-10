using System;
using System.IO;
using System.Net.Http.Headers;
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

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { dbPath = dbPath });
            }
            else
            {
                return BadRequest();
            }
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