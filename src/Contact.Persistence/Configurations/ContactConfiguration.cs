using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Persistence.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Domain.Entities.Contact>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Contact> builder)
        {
            AuditableEntityConfiguration<Domain.Entities.Contact>.SetProperties(builder);
            SoftDeleteConfiguration<Domain.Entities.Contact>.SetProperties(builder);

            builder.Property(m => m.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.LastName)
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
