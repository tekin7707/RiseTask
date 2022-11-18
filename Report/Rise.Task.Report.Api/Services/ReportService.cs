using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.Task.Report.Api.Db;
using Rise.Task.Report.Api.Domain;
using Rise.Task.Report.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rise.Task.Report.Api.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ReportDbContext _reportDbContext;
        private string BASE_API_URL = "";
        private string FILE_DIRECTORY = "";

        public ReportService(HttpClient httpClient, ReportDbContext reportDbContext, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _reportDbContext = reportDbContext;
            _configuration = configuration;
            BASE_API_URL = _configuration.GetSection("ContactMicroserviceUrl").Value;
            FILE_DIRECTORY = _configuration.GetSection("ReportFileDirectory").Value;
        }

        public async Task<Response<List<ContactReport>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}Contact");

            if (!response.IsSuccessStatusCode)
            {
                return Response<List<ContactReport>>.Fail("Any record not found",404);
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ContactReport>>>();

            return Response<List<ContactReport>>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<ContactReport>> GetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}Contact/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Response<ContactReport>.Fail("Any record not found", 404);

            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ContactReport>>();

            return Response<ContactReport>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<List<ContactReport>>> GetAllWithGeoAsync(string geo)
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}GetAllWithGeo/{geo}");

            if (!response.IsSuccessStatusCode)
            {
                return Response<List<ContactReport>>.Fail("Any record not found", 404);

            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ContactReport>>>();

            return Response<List<ContactReport>>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<NoContent>> AddAsync(ReportModel reportModel)
        {
            await _reportDbContext.Reports.AddAsync(reportModel);
            await _reportDbContext.SaveChangesAsync();
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportModel reportModel)
        {
            _reportDbContext.Reports.Update(reportModel);
            await _reportDbContext.SaveChangesAsync();
            return Response<NoContent>.Success(200);
        }
    }
}
