using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Application.Services;

namespace Rise.Task.Contact.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _contactService.DeleteAsync(id);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ContactDto contactDto)
        {
            var data = await _contactService.AddAsync(contactDto);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }
    }
}