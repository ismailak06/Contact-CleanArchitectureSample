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
    public class ContactConfiguration : IEntityTypeConfiguration<Domain.Entities.Contact>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Contact> builder)
        {
            AuditableEntityConfiguration<Domain.Entities.Contact>.SetProperties(builder);
            SoftDeleteConfiguration<Domain.Entities.Contact>.SetProperties(builder);

            builder.Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.Surname)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.CompanyName)
                .HasMaxLength(200);

            builder.Ignore(m => m.PhoneNumberInformations);
            builder.Ignore(m => m.EmailInformations);
            builder.Ignore(m => m.LocationInformations);

        }
    }
}
