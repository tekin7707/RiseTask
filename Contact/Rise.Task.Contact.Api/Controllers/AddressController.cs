using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Application.Services;

namespace Rise.Task.Contact.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IContactService _contactService;

        public AddressController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _contactService.DeleteAddressAsync(id);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddressAddDto addressAddDto)
        {
            var data = await _contactService.AddAddressAsync(addressAddDto);

            return new ObjectResult(data)
            {
                StatusCode = data.StatusCode
            };
        }

    }
}
