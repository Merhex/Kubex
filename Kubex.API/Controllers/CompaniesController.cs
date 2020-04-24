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
            try
            {
                var post = await _companyService.CreateAsync(dto);

                return Ok(post);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}