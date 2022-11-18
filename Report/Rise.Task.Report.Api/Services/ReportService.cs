using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.Task.Report.Api.Db;
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
        private readonly ReportDbContext _reportDbContext;
        private const string BASE_API_URL = "http://localhost:5501/";

        public ReportService(HttpClient httpClient, ReportDbContext reportDbContext)
        {
            _httpClient = httpClient;
            _reportDbContext = reportDbContext;
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
    }
}
