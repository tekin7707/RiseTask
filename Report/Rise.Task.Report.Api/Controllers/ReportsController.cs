using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Rise.Task.Report.Api.Services;
using Rise.Task.Shared.Models;

namespace Rise.Task.Report.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IReportService _reportService;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(ILogger<ReportsController> logger, IReportService reportService, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _reportService = reportService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpGet]
        [Route("GeoReport")]

        public async Task<IActionResult> PrepareReportAsync()
        {
            var data = await _reportService.PrepareReportAsync();

            var sendEndpoint1 = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:tekin06"));
            ContactReportCollection collection = new ContactReportCollection();
            collection.Items.AddRange(data.Data);
            await sendEndpoint1.Send<ContactReportCollection>(collection);

            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetReportsAsync()
        {
            var data = await _reportService.GetAllReportsAsync();

            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportAsync(int id)
        {
            var data = await _reportService.GetReportAsync(id);
            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
            return result;
        }

        [HttpGet]
        [Route("Contacts")]
        public async Task<IActionResult> GetAllContactAsync()
        {
            var data = await _reportService.GetAllContactsAsync();

            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };

            return result;
        }

        [HttpGet]
        [Route("Contacts/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data = await _reportService.GetContactAsync(id);
            var result = new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
            return result;
        }

    }
}