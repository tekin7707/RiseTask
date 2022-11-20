using Microsoft.AspNetCore.Mvc;
using Rise.Task.Report.Api.Dtos;
using Rise.Task.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Report.Api.Services
{
    public interface IReportService
    {
        Task<Response<List<ContactReportModel>>> PrepareReportAsync();
        Task<HttpResponseMessage> GetReportFileAsync(int reportId);

        Task<Response<List<ReportDto>>> GetAllReportsAsync();
        Task<Response<ReportDto>> GetReportAsync(int id);

        Task<Response<List<ContactDto>>> GetAllContactsAsync();
        Task<Response<ContactDto>> GetContactAsync(int id);
    }
}
