namespace Havillah.Core.Domain;

public class Product
{
    public Product(int productId, string productName, string productCode, string barcode, string description, string productImageUrl, int unitOfMeasureId, double defaultBuyingPrice, double defaultSellingPrice, int branchId, int currencyId)
    {
        ProductId = productId;
        ProductName = productName;
        ProductCode = productCode;
        Barcode = barcode;
        Description = description;
        ProductImageUrl = productImageUrl;
        UnitOfMeasureId = unitOfMeasureId;
        DefaultBuyingPrice = defaultBuyingPrice;
        DefaultSellingPrice = defaultSellingPrice;
        BranchId = branchId;
        CurrencyId = currencyId;
    }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public string ProductImageUrl { get; set; }
    public int UnitOfMeasureId { get; set; }
    public double DefaultBuyingPrice { get; set; } = 0.0;
    public double DefaultSellingPrice { get; set; } = 0.0;
    public int BranchId { get; set; }
    public int CurrencyId { get; set; }
}