using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Rise.Task.Report.Api.Models;
using Rise.Task.Report.Api.Services;

namespace Rise.Task.Report.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IReportService _reportService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, IReportService reportService, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _reportService = reportService;
            _sendEndpointProvider = sendEndpointProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _reportService.GetAllAsync();

            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data = await _reportService.GetAsync(id);
            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-report-service"));

            await sendEndpoint.Send<ContactReport>(data.Data);

            var resultx = new ObjectResult(new NoContent())
            {
                StatusCode = 200
            };

            return result;

            /*
            var data = await _reportService.GetAsync(id);
            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            return result;*/
        }
    }
}