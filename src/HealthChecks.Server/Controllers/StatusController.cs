using System;
using System.Threading.Tasks;
using HealthChecks.Server.Models;
using HealthChecks.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks.Server.Controllers
{
    [ApiController]
    [Route("status")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private static readonly string Token;

        static StatusController()
        {
            Token = Environment.GetEnvironmentVariable("Token");
        }

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        public async Task<IActionResult> Status(GetStatusRequest request)
        {
            if (!IsTokenValid(request.Token))
            {
                return Unauthorized();
            }

            var status = await _statusService.GetSystemStatusAsync();

            return Ok(status);
        }

        private static bool IsTokenValid(string requestToken)
        {
            return !string.IsNullOrWhiteSpace(requestToken) &&
                   requestToken.Equals(Token, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
