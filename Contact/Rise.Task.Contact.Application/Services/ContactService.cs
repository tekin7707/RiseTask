
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
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
                    Id=p.UUID,
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
                    Id = p.UUID,
                    IletisimTipi = p.IletisimTipi,
                    Iletisim = p.Iletisim
                })
            };

            return Response<ContactDto>.Success(result, 200);
        }

        public async Task<Response<ContactDto>> AddAsync(ContactDto model)
        {
            var dbModel = new Domain.Aggregate.ContactModel
            {
                Ad = model.Ad,
                Firma = model.Firma,
                Soyad = model.Soyad
            };

            await _contactDbContext.Contacts.AddAsync(dbModel);
            await _contactDbContext.SaveChangesAsync();

            if (model.Addresses?.Count() > 0)
            {
                var addresses = model.Addresses.Select(x => new AddressAddDto
                {
                    ContactId = dbModel.UUID,
                    Iletisim = x.Iletisim,
                    IletisimTipi = x.IletisimTipi
                }).ToList();
                await AddAddressRangeAsync(addresses);
            }

            var result = new ContactDto
            {
                Soyad = model.Soyad,
                Ad = model.Ad,
                Firma = model.Firma,
                Id = dbModel.UUID
            };

            return Response<ContactDto>.Success(result, 200);

        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var model = await _contactDbContext.Contacts.Where(x => x.UUID == id).FirstOrDefaultAsync();
            if (model == null)
            {
                return Response<NoContent>.Fail("Contact not found", 404);
            }
            else
            {
                var addresses = _contactDbContext.Addresses.Where(x => x.Contact.UUID == id).ToArray();
                _contactDbContext.Addresses.RemoveRange(addresses);
                _contactDbContext.Contacts.Remove(model);
                await _contactDbContext.SaveChangesAsync();
                return Response<NoContent>.Success(200);
            }
        }

        public async Task<Response<AddressDto>> AddAddressAsync(AddressAddDto model)
        {
            var contactModel = await _contactDbContext.Contacts.Where(x => x.UUID == model.ContactId).FirstOrDefaultAsync();
            if (contactModel == null)
            {
                return Response<AddressDto>.Fail("Contact not found", 404);
            }

            var dbModel = new Domain.Aggregate.AddressModel
            {
                Iletisim = model.Iletisim,
                IletisimTipi = model.IletisimTipi,
                Contact = contactModel
            };

            await _contactDbContext.Addresses.AddAsync(dbModel);
            await _contactDbContext.SaveChangesAsync();

            var result = new AddressDto
            {
                Id = dbModel.UUID,
                Iletisim = dbModel.Iletisim,
                IletisimTipi = dbModel.IletisimTipi
            };
            return Response<AddressDto>.Success(result, 200);
        }

        public async Task<Response<NoContent>> AddAddressRangeAsync(List<AddressAddDto> models)
        {

            var contactModel = await _contactDbContext.Contacts.Where(x => x.UUID == models.First().ContactId).FirstOrDefaultAsync();

            if (contactModel == null)
            {
                return Response<NoContent>.Fail("Contact not found", 404);
            }

            foreach (var model in models)
            {
                var dbModel = new Domain.Aggregate.AddressModel
                {
                    Iletisim = model.Iletisim,
                    IletisimTipi = model.IletisimTipi,
                    Contact = contactModel
                };
                await _contactDbContext.Addresses.AddAsync(dbModel);
            }

            await _contactDbContext.SaveChangesAsync();
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAddressAsync(int id)
        {
            var address = await _contactDbContext.Addresses.FirstOrDefaultAsync(x => x.UUID == id);
            if (address == null)
            {
                return Response<NoContent>.Fail("Address not found", 404);
            }
            else
            {
                _contactDbContext.Addresses.Remove(address);
                await _contactDbContext.SaveChangesAsync();
                return Response<NoContent>.Success(200);
            }
        }
    }
}
