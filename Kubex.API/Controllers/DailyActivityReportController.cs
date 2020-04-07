using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Kubex.API.Controllers
{
    [ApiController]
    [Route("dar")]
    public class DailyActivityReportController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            return Ok();
        }
    }
}