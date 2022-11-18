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

    }
}
