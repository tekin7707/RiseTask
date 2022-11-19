using MassTransit;
using Rise.Task.Contact.Application.Services;
using Rise.Task.Contact.Domain.Aggregate;
using Rise.Task.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MassTransit.Util.ChartTable;


namespace RiseTask.Contact.Application.Consumers
{
    public class GetContactCommandConsumer : IConsumer<ContactReportCollection>
    {
        private readonly IReportService _reportService;
        public GetContactCommandConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task Consume(ConsumeContext<ContactReportCollection> context)
        {
            try
            {
                var reportModel = new ReportModel
                {
                    IsReady = false,
                    CreatedDate = DateTime.Now
                };
                await _reportService.AddAsync(reportModel);

                var data = context.Message;
                var fileName = $"{123}_{context.MessageId}.txt";
                await File.WriteAllTextAsync($"wwwroot/reports/{fileName}", JsonSerializer.Serialize(data));

                reportModel.IsReady = true;
                reportModel.FilePath = fileName;
                await _reportService.UpdateAsync(reportModel);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
