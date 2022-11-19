using MassTransit;
using Rise.Task.Report.Api.Services;
using Rise.Task.Shared.Models;

namespace RiseTask.Report.Api.Consumers
{
    public class GetContactCommandConsumer : IConsumer<ContactReportModel>
    {
        private readonly IReportService _reportService;
        public GetContactCommandConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<ContactReportModel> context)
        {
            try
            {
                //var reportModel = new ReportModel
                //{
                //    IsReady = false,
                //    CreatedDate = DateTime.Now
                //};
                //await _reportService.AddAsync(reportModel);

                //var data = context.Message;
                //var fileName = $"{data.Ad}_{context.MessageId}.txt";
                ////await File.WriteAllTextAsync($"wwwroot/reports/{fileName}", JsonSerializer.Serialize(data));

                //reportModel.IsReady = true;
                //reportModel.FilePath = fileName;
                //await _reportService.UpdateAsync(reportModel);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
