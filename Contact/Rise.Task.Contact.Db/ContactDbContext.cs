using Microsoft.EntityFrameworkCore;
using Rise.Task.Contact.Domain.Aggregate;

namespace Rise.Task.Contact.Db
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactModel>().ToTable("Contacts");
            modelBuilder.Entity<AddressModel>().ToTable("Addresses");
            modelBuilder.Entity<ReportModel>().ToTable("Reports");
        }
    }
}