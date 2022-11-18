using Rise.Task.Report.Api.Domain;
using Rise.Task.Report.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Task.Report.Api.Services
{
    public interface IReportService
    {
        Task<Response<List<ContactReport>>> GetAllAsync();
        Task<Response<List<ContactReport>>> GetAllWithGeoAsync(string geo);
        Task<Response<ContactReport>> GetAsync(int id);
        Task<Response<NoContent>> AddAsync(ReportModel reportModel);
        Task<Response<NoContent>> UpdateAsync(ReportModel reportModel);

    }
}
