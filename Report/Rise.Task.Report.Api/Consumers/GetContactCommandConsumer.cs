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

namespace RiseTask.Report.Api.Consumers
{
    public class GetContactCommandConsumer : IConsumer<ContactReport>
    {
        private readonly ReportDbContext _reportDbContext;
        public GetContactCommandConsumer(ReportDbContext reportDbContext)
        {
            _reportDbContext = reportDbContext;
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
                await _reportDbContext.Reports.AddAsync(reportModel);
                await _reportDbContext.SaveChangesAsync();

                var data = context.Message;
                var fileName = $"{data.Id}_{context.MessageId}.txt";
                await File.WriteAllTextAsync($"wwwroot/reports/{fileName}", JsonSerializer.Serialize(data));

                reportModel.IsReady = true;
                reportModel.FilePath = fileName;
                _reportDbContext.Reports.Update(reportModel);
                await _reportDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }



            //TODO
            //var data = await _contactService.GetAsync(context.Message.Id);
            

            //await _contactDbContext.Orders.AddAsync(order);

            //await _contactDbContext.SaveChangesAsync();
        }
    }
}
