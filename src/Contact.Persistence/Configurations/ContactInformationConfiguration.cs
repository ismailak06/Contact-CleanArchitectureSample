using Contact.Domain.Entities;
using Contact.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Persistence.Configurations
{
    public class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {
            AuditableEntityConfiguration<ContactInformation>.SetProperties(builder);
            SoftDeleteConfiguration<ContactInformation>.SetProperties(builder);

            builder.Property(m => m.Content)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.InformationType)
                .HasConversion<short>();

            builder.HasOne(m => m.Contact).WithMany(m => m.ContactInformations);

            builder.Ignore(m => m.InformationTypeDisplayName);
        }
    }
}
