using Microsoft.AspNetCore.Mvc;

namespace Rise.Task.Report.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public string Get()
        {
            return "";
        }
    }
}