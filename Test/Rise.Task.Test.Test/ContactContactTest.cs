using Microsoft.Extensions.Configuration;
using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Application.Services;
using Rise.Task.Contact.Db;
using Rise.Task.Contact.Domain.Aggregate;
using Rise.Task.Test.Test;
using Rise.Task.Test.Test.testDatas;

namespace RiseTask.Test.Test
{
    public class ContactTest
    {
        private readonly IContactService _contactService;
        private readonly ContactDbContext _context;

        public ContactTest()
        {
            _context = MockDBContext.Get();
            _contactService = new ContactService(_context);
        }

        [Fact]
        public async Task GetAllContactAsync()
        {
            var response = await _contactService.GetAllAsync();
            Assert.NotEmpty(response.Data);

        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(0, 404)]
        public async Task GetContactAsync(int id, int statusCode)
        {
            var response = await _contactService.GetAsync(id);
            Assert.Equal(response.StatusCode, statusCode);
        }


        [Theory]
        [ClassData(typeof(AddContactTest))]
        public async Task AddNewReportAsync(ContactDto newContact, int statusCode)
        {
            var response = await _contactService.AddAsync(newContact);
            Assert.Equal(response.StatusCode, statusCode);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(0, 404)]
        public async Task DeleteContactTestAsync(int id, int statusCode)
        {
            var response = await _contactService.DeleteAsync(id);
            Assert.Equal(response.StatusCode, statusCode);
        }

    }
}