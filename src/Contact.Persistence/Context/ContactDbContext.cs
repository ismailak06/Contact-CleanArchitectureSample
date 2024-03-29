﻿using Contact.Application.Common.Interfaces;
using Contact.Domain.Common;
using Contact.Domain.Entities;
using Contact.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Contact.Persistence.Context
{
    public class ContactDbContext : DbContext, IContactDbContext
    {
        public DbSet<Domain.Entities.Contact> Contacts { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<DocumentLog> DocumnetLogs { get; set; }

        public ContactDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.SoftDelete();
                        break;
                    default:
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Database.BeginTransactionAsync();
        }
    }
}
