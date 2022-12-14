using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class PurchasePartyConfiguration: IEntityTypeConfiguration<PurchaseParty>
{
    public void Configure(EntityTypeBuilder<PurchaseParty> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PartyName).HasColumnType("nvarchar(300)").IsRequired(true);
        builder.Property(x => x.Email).HasColumnType("nvarchar(1000)").IsRequired(false);
        builder.Property(x => x.Website).HasColumnType("nvarchar(1000)").IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar(1000)").IsRequired(true);
    }
}