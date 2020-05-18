using System;
using System.Globalization;
using System.Threading.Tasks;
using Kubex.BLL.Services.Interfaces;
using Kubex.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Route("dar")]
    [Authorize]
    public class DailyActivityReportController : ControllerBase
    {
        private readonly IDailyActivityReportService _darService;

        public DailyActivityReportController(IDailyActivityReportService darService)
        {
            _darService = darService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDARbyId(int id) 
        {
            var dar = await _darService.GetDailyActivityReportAsync(id);

            return Ok(dar);
        }

        [HttpGet("last")]
        public async Task<IActionResult> GetLastDAR() 
        {
            var dar = await _darService.GetLastDailyActivityReportAsync();

            return Ok(dar);
        }

        [HttpPost("create", Name = "CreateDAR")]
        public async Task<IActionResult> CreateDAR()
        {
            var dar = await _darService.CreateDailyActivityReportAsync();

            return CreatedAtRoute
            (
                "CreateDAR",
                new { controller = "DailyActivityReport" },
                dar
            );
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEntry(AddEntryToDailyActivityReportDTO dto) 
        {
            var dar = await _darService.AddEntryAsync(dto);
            
            return Ok(dar);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEntryInDailyActivityReport(AddEntryToDailyActivityReportDTO dto) 
        {
            await _darService.UpdateEntryInDailyActivityReportAsync(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDAR(int id) 
        {
            await _darService.DeleteDailyActivityReportAsync(id);

            return NoContent();
        }

        [HttpDelete("{darId}/{entryId}")]
        public async Task<IActionResult> DeleteEntryFromDAR(int darId, int entryId) 
        {
            await _darService.DeleteEntryFromDailyActivityReportAsync(entryId, darId);

            return NoContent();
        }
    }
}