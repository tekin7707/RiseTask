using Rise.Task.Contact.Application.Dtos;
using Rise.Task.Contact.Domain.Aggregate;
using Rise.Task.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Contact.Application.Services
{
    public interface IReportService
    {
        Task<Response<List<ReportDto>>> GetAllAsync();
        Task<Response<ReportDto>> GetAsync(int id);
        Task<Response<NoContent>> AddAsync(ReportModel reportModel);
        Task<Response<NoContent>> UpdateAsync(ReportModel reportModel);
        Task<Response<List<ContactReportModel>>> PrepareReportAsync();


    }
}
