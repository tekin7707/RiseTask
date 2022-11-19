using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.Task.Contact.Application.Services;

namespace Rise.Task.Contact.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _reportService.GetAllAsync();

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data = await _reportService.GetAsync(id);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpGet]
        [Route("/GeoReports")]
        public async Task<IActionResult> GetAllWithGeoAsync()
        {
            var data = await _reportService.PrepareReportAsync();

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

    }
}
