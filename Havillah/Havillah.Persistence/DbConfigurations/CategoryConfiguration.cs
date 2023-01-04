using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class CategoryConfiguration: IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasColumnType("nvarchar(300)").IsRequired(true);
        builder.Property(x => x.DateAdded).HasColumnType("Datetime2").IsRequired(true);
    }
}