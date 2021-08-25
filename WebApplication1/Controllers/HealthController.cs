using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Health controller used to check service status for now just a basic ping.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private ILogger<string> logger;

        public HealthController(ILogger<string> logger) => this.logger = logger;

        [HttpGet]
        public string Ping()
        {
            this.logger.LogInformation($"Ping Check: {DateTimeOffset.UtcNow}");
            return "I AM ALIVE";
        }
    }
}
