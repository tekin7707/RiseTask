using Microsoft.EntityFrameworkCore;
using Rise.Task.Contact.Db;
using Rise.Task.Contact.Domain.Aggregate;
using System;
using System.Collections.Generic;

namespace Rise.Task.Test.Test
{
    public class MockDBContext
    {
        public static ContactDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString())
              .Options;

            var context = new ContactDbContext(options);
            context.Database.EnsureCreated();
            Fill(context);
            return context;
        }

        public static void Fill(ContactDbContext context)
        {
            for (int i = 0; i < 20; i++)
            {
                context.Contacts.Add(new ContactModel { Ad = $"Contact {i + 1}", Soyad = $"Soyad {i + 1}", Firma = $"Firma {i + 1}" });
            }
            context.Contacts.Add(new ContactModel { UUID = 99, Ad = $"Contact 99", Soyad = $"Soyad 99", Firma = $"Firma 99" });

            context.SaveChanges();

            var contacts = context.Contacts.OrderBy(x => x.UUID).ToArray();

            for (int i = 0; i < contacts.Length; i++)
            {
                if (contacts[i].UUID == 99)
                {
                    contacts[i].Addresses = new List<AddressModel> { new AddressModel { IletisimTipi = 0, Iletisim = $"053200000099", Contact = contacts[i] } };
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"053300000077", Contact = contacts[i] });
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Konum, Iletisim = $"konum99", Contact = contacts[i] });
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Email, Iletisim = $"mail99@gmail.com", Contact = contacts[i] });
                    continue;
                }
                contacts[i].Addresses = new List<AddressModel> { new AddressModel { IletisimTipi = 0, Iletisim = $"0533000000{i + 1}", Contact = contacts[i] } };
                contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Konum, Iletisim = $"konum{i % 5}", Contact = contacts[i] });

                if (i % 3 == 0)
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"0533000001{i + 1}", Contact = contacts[i] });
                else
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Email, Iletisim = $"mail{i + 1}@gmail.com", Contact = contacts[i] });

                if (i == 5)
                {
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"0533000002{i + 1}", Contact = contacts[i] });
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = 0, Iletisim = $"0533000003{i + 1}", Contact = contacts[i] });
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Email, Iletisim = $"mail{i + 1}@gmail2.com", Contact = contacts[i] });
                    contacts[i].Addresses.Add(new AddressModel { IletisimTipi = Rise.Task.Contact.Domain.Enums.AddressType.Konum, Iletisim = $"konum{i % 5}", Contact = contacts[i] });
                }
            }

            context.Reports.Add(new ReportModel { CreatedDate = DateTime.Now.AddDays(-5), UUID = 8, IsReady = false });
            context.Reports.Add(new ReportModel { CreatedDate = DateTime.Now.AddDays(-15), UUID = 9, IsReady = true, FilePath="123.txt"  });

            context.SaveChanges();


        }
    }
}