using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Rise.Task.Contact.Application.Services;
using Rise.Task.Contact.Db;
using Rise.Task.Contact.Domain.Aggregate;
using Rise.Task.Test.Test;
using Rise.Task.Test.Test.testDatas;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace RiseTask.Test.Test
{
    public class ReportTest
    {
        private readonly IReportService _reportService;
        private readonly ContactDbContext _context;

        public ReportTest()
        {
            _context = MockDBContext.Get();

            var myConfiguration = new Dictionary<string, string>
            {
                {"RabbitMQUrl", "localhost"},
                {"ReportFileDirectory", "wwwroot/reports/"}
            };
            var _configuration = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();

            _reportService = new ReportService(_configuration, _context);
        }



        [Fact]
        public async Task GetAllCreatedReportsAsync()
        {
            var response = await _reportService.GetAllAsync();
            Assert.NotEmpty(response.Data);
        }

        [Theory]
        [InlineData(8, 200)]
        [InlineData(0, 404)]
        public async Task GetContactAsync(int id, int statusCode)
        {
            var response = await _reportService.GetAsync(id);
            Assert.Equal(response.StatusCode, statusCode);
        }

        [Theory]
        [ClassData(typeof(AddReportModelTest))]
        public async Task AddNewReportAsync(ReportModel newReport, int statusCode)
        {
            var response = await _reportService.AddAsync(newReport);
            Assert.Equal(response.StatusCode, statusCode);
        }

        [Theory]
        [InlineData("konum99", 2)]
        public async Task GetGeoReportAsync(string konum, int itemsCount)
        {
            var response = await _reportService.PrepareReportAsync();
            var phoneCount = response.Data.FirstOrDefault(x => x.Konum == konum)?.TelNum;
            Assert.NotNull(response.Data);
            Assert.Equal(phoneCount, itemsCount);
        }


    }
}