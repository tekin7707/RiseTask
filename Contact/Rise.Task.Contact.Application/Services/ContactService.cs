
using Microsoft.EntityFrameworkCore;
using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Application.Models;
using Rise.Task.Contact.Db;
using Rise.Task.Contact.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactDbContext _contactDbContext;

        public ContactService(ContactDbContext contactDbContext)
        {
            _contactDbContext = contactDbContext;
        }

        public Task<ContactDto> AddAsync(ContactDto model)
        {
            throw new NotImplementedException();
        }

        public Task<NoContent> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<ContactDto>>> GetAllWithGeoAsync(string geo)
        {
            var model = await _contactDbContext.Contacts.Where(x => x.Addresses.Any(g => g.IletisimTipi == AddressType.Konum && g.Iletisim == geo)).Include(x => x.Addresses).ToListAsync();

            if (model == null)
            {
                return Response<List<ContactDto>>.Fail("Any contacts not found", 404);
            }

            var result = model.Select(x => new ContactDto
            {
                Id = x.UUID,
                Ad = x.Ad,
                Firma = x.Firma,
                Soyad = x.Soyad,
                Addresses = x.Addresses.Select(p => new AddressDto
                {
                    IletisimTipi = p.IletisimTipi,
                    Iletisim = p.Iletisim
                })
            }).ToList();

            return Response<List<ContactDto>>.Success(result, 200);
        }

        public async Task<Response<List<ContactDto>>> GetAllAsync()
        {
            var model = await _contactDbContext.Contacts.Include(x => x.Addresses).ToListAsync();

            if (model == null)
            {
                return Response<List<ContactDto>>.Fail("Any contacts not found", 404);
            }

            var result = model.Select(x => new ContactDto
            {
                Id = x.UUID,
                Ad = x.Ad,
                Firma = x.Firma,
                Soyad = x.Soyad,
                Addresses = x.Addresses.Select(p => new AddressDto
                {
                    IletisimTipi = p.IletisimTipi,
                    Iletisim = p.Iletisim
                })
            }).ToList();

            return Response<List<ContactDto>>.Success(result, 200);

        }

        public async Task<Response<ContactDto>> GetAsync(int id)
        {
            var model = await _contactDbContext.Contacts.Where(x => x.UUID == id).Include(x => x.Addresses).FirstOrDefaultAsync();

            if (model == null)
            {
                return Response<ContactDto>.Fail("Contact not found", 404);
            }

            var result = new ContactDto
            {
                Id = model.UUID,
                Ad = model.Ad,
                Firma = model.Firma,
                Soyad = model.Soyad,
                Addresses = model.Addresses.Select(p => new AddressDto
                {
                    IletisimTipi = p.IletisimTipi,
                    Iletisim = p.Iletisim
                })
            };

            return Response<ContactDto>.Success(result, 200);
        }

        public Task<AddressDto> AddAddressAsync(AddressAddDto model)
        {
            throw new NotImplementedException();
        }

        public Task<NoContent> DeleteAddressAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
