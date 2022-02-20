using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Persistence.Configurations
{
    public class DocumentLogConfiguration : IEntityTypeConfiguration<DocumentLog>
    {
        public void Configure(EntityTypeBuilder<DocumentLog> builder)
        {
            AuditableEntityConfiguration<DocumentLog>.SetProperties(builder);
            SoftDeleteConfiguration<DocumentLog>.SetProperties(builder);

            builder.Property(m => m.ProcessStatus)
                .IsRequired();

            builder.Ignore(m => m.ProcessStatusDisplayName);
        }
    }
}
