using Rise.Task.Report.Api.Dtos;
using Rise.Task.Shared.Models;

namespace Rise.Task.Report.Api.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string BASE_API_URL = "";

        public ReportService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BASE_API_URL = _configuration.GetSection("ContactMicroserviceUrl").Value;
        }

        public async Task<Response<List<ContactDto>>> GetAllContactsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}Contacts");

            if (!response.IsSuccessStatusCode)
            {
                return Response<List<ContactDto>>.Fail("Any record not found",404);
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ContactDto>>>();

            return Response<List<ContactDto>>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<List<ReportDto>>> GetAllReportsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}Reports");

            if (!response.IsSuccessStatusCode)
            {
                return Response<List<ReportDto>>.Fail("Any record not found", 404);
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ReportDto>>>();

            return Response<List<ReportDto>>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<ContactDto>> GetContactAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}Contacts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Response<ContactDto>.Fail("Any record not found", 404);

            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ContactDto>>();

            return Response<ContactDto>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<ReportDto>> GetReportAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}Reports/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return Response<ReportDto>.Fail("Any record not found", 404);

            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ReportDto>>();

            return Response<ReportDto>.Success(responseSuccess.Data, 200);
        }

        public async Task<Response<List<ContactReportModel>>> PrepareReportAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_API_URL}GeoReports");

            if (!response.IsSuccessStatusCode)
            {
                return Response<List<ContactReportModel>>.Fail("Any record not found", 404);

            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ContactReportModel>>>();

            return Response<List<ContactReportModel>>.Success(responseSuccess.Data, 200);
        }


    }
}
