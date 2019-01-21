using System.Diagnostics;
using System.Threading.Tasks;
using HealthChecks.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks.Server.Controllers
{
    [Route("status")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> Status()
        {
            var status = await _statusService.GetSystemStatusAsync();

            return Ok(status);
        }
    }
}
