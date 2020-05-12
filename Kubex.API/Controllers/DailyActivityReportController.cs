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
        public async Task<IActionResult> GetDAR(int id) 
        {
            var dar = await _darService.GetDailyActivityReportAsync(id);

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
            var dar = new DailyActivityReportDTO { };

            if (dto.ParentEntry != null)
                dar = await _darService.AddChildEntryAsync(dto);
            else
                dar = await _darService.AddEntryAsync(dto);
            
            return Ok(dar);
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