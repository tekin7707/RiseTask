using Microsoft.EntityFrameworkCore;

namespace Rise.Task.Contact.Db
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}