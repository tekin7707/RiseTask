using ClosedXML.Excel;
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
                var fileName = $"geo-report-{context.MessageId}.xlsx";
                //await File.WriteAllTextAsync($"wwwroot/reports/{fileName}.txt", JsonSerializer.Serialize(data));

                var workbook = new XLWorkbook();
                workbook.AddWorksheet("sheetName");
                var ws = workbook.Worksheet("sheetName");

                int row = 1;

                ws.Cell("A" + row.ToString()).Value = "Konum";
                ws.Cell("B" + row.ToString()).Value = "Kişi sayısı";
                ws.Cell("C" + row.ToString()).Value = "Telefon sayısı";

                foreach (var item in data.Items)
                {
                    row++;
                    ws.Cell("A" + row.ToString()).Value = item.Konum;
                    ws.Cell("B" + row.ToString()).Value = item.Kisi;
                    ws.Cell("C" + row.ToString()).Value = item.TelNum;
                }
                workbook.SaveAs($"wwwroot/reports/{fileName}");

                reportModel.IsReady = true;
                reportModel.FilePath = fileName;
                await _reportService.UpdateAsync(reportModel);
            }
            catch (Exception)
            {
                //TODO log
            }
        }
    }
}
