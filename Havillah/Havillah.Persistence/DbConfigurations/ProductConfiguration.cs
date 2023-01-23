using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Havillah.Persistence.DbConfigurations;

public class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductName).HasColumnType("nvarchar(300)").IsRequired(true);
        builder.Property(x => x.ProductCode).HasColumnType("nvarchar(1000)").IsRequired(true);
        builder.Property(x => x.Barcode).HasColumnType("nvarchar(1000)").IsRequired(false);
        builder.Property(x => x.Description).HasColumnType("nvarchar(1000)").IsRequired(true);
        builder.Property(x => x.ProductImageUrl).HasColumnType("nvarchar(100)").IsRequired(false);
        builder.Property(x => x.BrandName).HasColumnType("nvarchar(90)").IsRequired(false);
        builder.Property(x => x.Colours).HasColumnType("nvarchar(300)").IsRequired(false);
        builder.Property(x => x.Sizes).HasColumnType("nvarchar(200)").IsRequired(false);
        builder.Property(x => x.ProductImage).IsRequired(true);
        builder.Property(x => x.ProductImageExtension).IsRequired(true);
        builder.Property(x => x.ProductImageLength).IsRequired(true);
        builder.Property(x => x.Quantity).HasColumnType("int").IsRequired(true);
        builder.Property(x => x.UnitOfMeasureId).HasColumnType("int");
        builder.Property(x => x.BuyingPrice).HasPrecision(18, 2).IsRequired(true);
        builder.Property(x => x.SellingPrice).HasPrecision(18, 2).IsRequired(true);
        builder.Property(x => x.BranchId).HasColumnType("int");
        builder.Property(x => x.CurrencyId).HasColumnType("int");
        builder.Property(x => x.Created).HasColumnType("Datetime2").IsRequired(true);
        builder.Property(x => x.DateAdded).HasColumnType("Datetime2").IsRequired(true);
        builder.Property(x => x.CreatedBy).HasColumnType("nvarchar").IsRequired(false);
        builder.Property(x => x.CategoryId).HasColumnType("int").IsRequired(true);
        builder.HasMany(x => x.Stocks).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
    }
}