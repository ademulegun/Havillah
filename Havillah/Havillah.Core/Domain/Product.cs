namespace Havillah.Core.Domain;

public class Product: BaseEntity<Guid>
{
    protected Product() { }
    private Product(Guid id, string productName, string productCode, string description, string productImageUrl, int unitOfMeasureId, double buyingPrice, double sellingPrice)
    {
        Id = id;
        ProductName = productName;
        ProductCode = productCode;
        Description = description;
        ProductImageUrl = productImageUrl;
        UnitOfMeasureId = unitOfMeasureId;
        BuyingPrice = buyingPrice;
        SellingPrice = sellingPrice;
        DateAdded = DateTime.Now;
    }
    
    public string ProductName { get; private set; }
    public string ProductCode { get; private set; }
    public string? Barcode { get; set; }
    public string Description { get; private set; }
    public string ProductImageUrl { get; private set; }
    public int UnitOfMeasureId { get; set; }
    public double BuyingPrice { get; private set; } = 0.0;
    public double SellingPrice { get; private set; } = 0.0;
    public int BranchId { get; set; }
    public int CurrencyId { get; set; }

    public static class ProductFactory
    {
        public static Product Create(Guid id, string productName, string productCode, string description,
            string productImageUrl, int unitOfMeasureId, double buyingPrice, double sellingPrice)
        {
            return new Product(id, productName, productCode, description, productImageUrl, unitOfMeasureId, buyingPrice,
                sellingPrice);
        }
    }

    public Product SetUnitOfMeasureId(int unitOfMeasureId)
    {
        this.UnitOfMeasureId = unitOfMeasureId;
        return this;
    }
    
    public Product SetBranchId(int branchId)
    {
        this.BranchId = branchId;
        return this;
    }
    
    public Product SetCurrencyId(int currencyId)
    {
        this.CurrencyId = currencyId;
        return this;
    }
}