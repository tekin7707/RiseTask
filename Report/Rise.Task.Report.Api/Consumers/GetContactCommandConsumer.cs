using MassTransit;
using Rise.Task.Report.Api;
using Rise.Task.Report.Api.Domain;
using Rise.Task.Report.Api.Db;
using Rise.Task.Report.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MassTransit.Util.ChartTable;
using Rise.Task.Report.Api.Services;

namespace RiseTask.Report.Api.Consumers
{
    public class GetContactCommandConsumer : IConsumer<ContactReport>
    {
        private readonly IReportService _reportService;
        public GetContactCommandConsumer(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task Consume(ConsumeContext<ContactReport> context)
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
                var fileName = $"{data.Id}_{context.MessageId}.txt";
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
