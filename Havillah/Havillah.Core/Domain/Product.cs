namespace Havillah.Core.Domain;

public class Product: BaseEntity<Guid>
{
    protected Product() { }
    private Product(Guid id, string productName, string productCode, string description, string productImageUrl, int unitOfMeasureId, 
        double buyingPrice, double sellingPrice, byte[] productImage, long productImageLength, string productImageExtension, 
        string colours, string sizes, string brandName)
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
        ProductImage = productImage;
        ProductImageLength = productImageLength;
        this.ProductImageExtension = productImageExtension;
        Colours = colours;
        Sizes = sizes;
        BrandName = brandName;
    }
    
    public string ProductName { get; private set; }
    public string ProductCode { get; private set; }
    public string BrandName { get; private set; }
    public string? Barcode { get; set; }
    public string Description { get; private set; }
    public string Sizes { get; private set; }
    public string Colours { get; private set; }
    public string ProductImageUrl { get; private set; }
    public int UnitOfMeasureId { get; set; } = 0;
    public int Quantity { get; set; }
    public double BuyingPrice { get; private set; } = 0.0;
    public double SellingPrice { get; private set; } = 0.0;
    public int BranchId { get; set; } = 0;
    public int CurrencyId { get; set; } = 0;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public byte[] ProductImage { get; private set; }
    public long ProductImageLength { get; private set; }
    public string ProductImageExtension { get; private set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public List<Stock> Stocks { get; private set; }

    public static class ProductFactory
    {
        public static Product Create(Guid id, string productName, string productCode, string description,
            string productImageUrl, int unitOfMeasureId, double buyingPrice, double sellingPrice, byte[] productImage, 
            long productImageLength, string productImageExtension, string colours, string sizes, string brandName)
        {
            return new Product(id, productName, productCode, description, productImageUrl, unitOfMeasureId, buyingPrice,
                sellingPrice, productImage, productImageLength, productImageExtension, colours, sizes, brandName);
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