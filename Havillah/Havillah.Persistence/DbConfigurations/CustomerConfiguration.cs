using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).HasColumnType("nvarchar(300)").IsRequired(true);
        builder.Property(x => x.Email).HasColumnType("nvarchar(1000)").IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(100)").IsRequired(true);
        builder.Property(x => x.IsWhatsAppEnabled).HasColumnType("bit").IsRequired(true);
    }
}