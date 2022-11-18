using Microsoft.AspNetCore.Mvc;
using Rise.Task.Contact.Application.Services;

namespace Rise.Task.Contact.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;

        public ContactController(ILogger<ContactController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _contactService.GetAllAsync();

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data = await _contactService.GetAsync(id);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpGet]
        [Route("/GetAllWithGeo/{geo}")]
        public async Task<IActionResult> GetAllWithGeoAsync(string geo)
        {
            var data = await _contactService.GetAllWithGeoAsync(geo);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var data = await _contactService.GetAllAsync();

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }
    }
}