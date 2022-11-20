using MassTransit;
using Microsoft.EntityFrameworkCore;
using Rise.Task.Contact.Application.Services;
using Rise.Task.Contact.Db;
using Rise.Task.Contact.Domain.Aggregate;
using RiseTask.Contact.Application.Consumers;

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

        cfg.ReceiveEndpoint("tekin06", e =>
        {
            e.ConfigureConsumer<GetContactCommandConsumer>(context);
        });
    });
});

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var appDbContext = serviceProvider.GetRequiredService<ContactDbContext>();
    appDbContext.Database.Migrate();

    var context = new ContactDbContext(serviceProvider.GetRequiredService<DbContextOptions<ContactDbContext>>());
    if (!context.Contacts.Any())
    {
        
        for (int i = 0; i < 20; i++)
        {
            context.Contacts.Add(new ContactModel { Ad = $"Contact {i + 1}", Soyad = $"Soyad {i + 1}", Firma = $"Firma {i + 1}" });
        }
        await context.SaveChangesAsync();


        var contacts = context.Contacts.OrderBy(x=>x.UUID).ToArray();

        for (int i = 0; i < contacts.Length; i++)
        {
            contacts[i].Addresses = new List<AddressModel> { new AddressModel { IletisimTipi = 0, Iletisim = $"0533000000{i + 1}", Contact = contacts[i] } };
            contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Konum, Iletisim = $"konum{i % 5}", Contact = contacts[i] });

            if (i % 3 == 0)
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"0533000001{i + 1}", Contact = contacts[i]});
            else
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Email, Iletisim = $"mail{i + 1}@gmail.com", Contact = contacts[i] });

            if (i == 5)
            {
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"0533000002{i + 1}", Contact = contacts[i] });
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"0533000003{i + 1}", Contact = contacts[i] });
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Email, Iletisim = $"mail{i + 1}@gmail2.com", Contact = contacts[i] });
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Konum, Iletisim = $"konum{i%5}", Contact = contacts[i] });
            }
        }
        await context.SaveChangesAsync();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
