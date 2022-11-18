using MassTransit;
using Microsoft.EntityFrameworkCore;
using Rise.Task.Report.Api.Db;
using Rise.Task.Report.Api.Services;
using RiseTask.Report.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<GetContactCommandConsumer>();
    // Default Port : 5672
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("create-report-service", e =>
        {
            e.ConfigureConsumer<GetContactCommandConsumer>(context);
        });
    });
});

builder.Services.AddDbContext<ReportDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
