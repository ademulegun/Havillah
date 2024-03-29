namespace Havillah.Shared.Product;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public string ProductImage { get; set; }
    public int UnitOfMeasureId { get; set; }
    public double DefaultBuyingPrice { get; set; } = 0.0;
    public double DefaultSellingPrice { get; set; } = 0.0;
    public int BranchId { get; set; }
    public int CurrencyId { get; set; }
    public int Quantity { get; set; }
    public string BrandName { get; set; }
    public string Sizes { get; set; }
    public string Colours { get; set; }
}