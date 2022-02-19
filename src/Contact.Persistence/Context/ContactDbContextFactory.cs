using Contact.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
