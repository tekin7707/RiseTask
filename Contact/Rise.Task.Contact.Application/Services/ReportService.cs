using MassTransit.Futures.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Db;
using Rise.Task.Contact.Domain.Aggregate;
using Rise.Task.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IConfiguration _configuration;
        private readonly ContactDbContext _contactDbContext;
        private string FILE_DIRECTORY = "reports/";
        private string APPLICATION_URL = "http://localhost:5501/";

        public ReportService(IConfiguration configuration, ContactDbContext contactDbContext)
        {
            _configuration = configuration;
            //FILE_DIRECTORY = _configuration.GetSection("ReportFileDirectory").Value;
            _contactDbContext = contactDbContext;
        }

        public async Task<Response<List<ContactReportModel>>> PrepareReportAsync()
        {
            List<ContactReportModel> contactReportModels = new List<ContactReportModel>();

            var contacts = await _contactDbContext.Contacts.Include(x => x.Addresses).ToListAsync();

            var addresses = await _contactDbContext.Addresses.Where(x => x.IletisimTipi == Domain.Enums.AddressType.Konum)
                .Select(x => x.Iletisim).Distinct().OrderBy(x => x).ToListAsync();

            foreach (var konum in addresses)
            {
                var list = await _contactDbContext.Contacts.Where(x => x.Addresses.Any(g => g.IletisimTipi == Domain.Enums.AddressType.Konum && g.Iletisim == konum)).Include(x => x.Addresses).ToListAsync();
                var ritem = new ContactReportModel();
                ritem.Konum = konum;
                ritem.Kisi = list.Count();
                ritem.TelNum = list.Sum(x => x.Addresses.Where(a => a.IletisimTipi == Domain.Enums.AddressType.Telefon).Count());
                contactReportModels.Add(ritem);
            }
            return Response<List<ContactReportModel>>.Success(contactReportModels, 200);
        }

        public async Task<Response<NoContent>> AddAsync(ReportModel reportModel)
        {
            await _contactDbContext.Reports.AddAsync(reportModel);
            int id = await _contactDbContext.SaveChangesAsync();
            return id > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("Record couldn't created.", 404);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportModel reportModel)
        {
            _contactDbContext.Reports.Update(reportModel);
            int id = await _contactDbContext.SaveChangesAsync();
            return id > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("Record couldn't updated.", 404);
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var model = await _contactDbContext.Reports.ToListAsync();

            if (model == null)
            {
                return Response<List<ReportDto>>.Fail("Any report not found", 404);
            }

            var result = model.Select(x => new ReportDto
            {
                Id = x.UUID,
                CreatedDate = x.CreatedDate,
                FilePath = $"{APPLICATION_URL}{FILE_DIRECTORY}{x.FilePath}",
                IsReady = x.IsReady
            }).ToList();

            return Response<List<ReportDto>>.Success(result, 200);

        }

        public async Task<Response<ReportDto>> GetAsync(int id)
        {
            var model = await _contactDbContext.Reports.FirstOrDefaultAsync(x => x.UUID == id);

            if (model == null)
            {
                return Response<ReportDto>.Fail("Any report not found", 404);
            }

            var result = new ReportDto
            {
                Id = model.UUID,
                CreatedDate = model.CreatedDate,
                FilePath = $"{APPLICATION_URL}{FILE_DIRECTORY}{model.FilePath}",
                IsReady = model.IsReady
            };

            return Response<ReportDto>.Success(result, 200);
        }
    }
}
