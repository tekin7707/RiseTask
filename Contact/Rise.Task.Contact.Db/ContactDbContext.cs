using Microsoft.EntityFrameworkCore;
using Rise.Task.Contact.Domain.Aggregate;

namespace Rise.Task.Contact.Db
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }
        public DbSet<ContactModel> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactModel>().ToTable("Contacts");
            modelBuilder.Entity<AddressModel>().ToTable("Addresses");
        }
    }
}