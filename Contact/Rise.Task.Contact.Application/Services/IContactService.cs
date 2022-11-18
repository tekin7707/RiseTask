using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Services
{
    public interface IContactService
    {
        Task<Response<ContactDto>> GetAsync(int id);
        Task<Response<List<ContactDto>>> GetAllAsync();
        Task<ContactDto> AddAsync(ContactDto model); 
        Task<NoContent> DeleteAsync(int id);
        Task<AddressDto> AddAddressAsync(AddressAddDto model);
        Task<NoContent> DeleteAddressAsync(int id);

        Task<Response<List<ContactDto>>> GetAllWithGeoAsync(string geo);

    }
}
