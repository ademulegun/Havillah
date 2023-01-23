using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class StockConfiguration: IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Quantity).HasColumnType("int").IsRequired(true);
        builder.Property(x => x.Created).HasColumnType("Datetime2").IsRequired(true);
        builder.Property(x => x.Price).HasPrecision(18, 2).IsRequired(true);
    }
}