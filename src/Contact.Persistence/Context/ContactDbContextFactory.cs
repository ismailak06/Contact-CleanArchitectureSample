using Contact.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Contact.Persistence.Context
{
    public class ContactDbContextFactory : DesignTimeDbContextFactoryBase<ContactDbContext>
    {
        protected override ContactDbContext CreateNewInstance(DbContextOptions<ContactDbContext> options)
        {
            return new ContactDbContext(options);
        }
    }
}
