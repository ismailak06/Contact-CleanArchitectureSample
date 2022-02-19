using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contact.Application.Common.Interfaces
{
    public interface IContactDbContext
    {
        DbSet<Domain.Entities.Contact> Contacts { get; set; }
        DbSet<ContactInformation> ContactInformations { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
