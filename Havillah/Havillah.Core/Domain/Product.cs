namespace Havillah.Core.Domain;

public class Product: BaseEntity<Guid>
{
    protected Product() { }
    private Product(Guid id, string productName, string productCode, string description, string productImageUrl, int unitOfMeasureId, 
        double buyingPrice, double sellingPrice, byte[] productImage, long productImageLength, string productImageExtension, 
        string colours, string sizes, string brandName, int quantity, ProductCategory category)
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
        Quantity = quantity;
        Category = category;
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
    public int Quantity { get; private set; }
    public double BuyingPrice { get; private set; } = 0.0;
    public double SellingPrice { get; private set; } = 0.0;
    public int BranchId { get; set; } = 0;
    public int CurrencyId { get; set; } = 0;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public byte[] ProductImage { get; private set; }
    public long ProductImageLength { get; private set; }
    public string ProductImageExtension { get; private set; }
    public int CategoryId { get; private set; }
    public ProductCategory Category { get; private set; }
    public List<Stock> Stocks { get; private set; }

    public static class ProductFactory
    {
        public static Product Create(Guid id, string productName, string productCode, string description, string productImageUrl, 
            int unitOfMeasureId, double buyingPrice, double sellingPrice, byte[] productImage, long productImageLength, 
            string productImageExtension, string colours, string sizes, string brandName, int quantity, ProductCategory category)
        {
            //if (quantity < 1) throw new Exception("Quantity can not be less than 1");
            //if (buyingPrice < 100) throw new Exception("");
            //if (categoryId < 1) throw new Exception("");
            return new Product(id, productName, GenerateProductCode(productCode), description, productImageUrl, unitOfMeasureId, buyingPrice,
                sellingPrice, productImage, productImageLength, productImageExtension, colours, sizes, brandName, quantity, category);
        }
    }
    public Product SetProductName(string productName)
    {
        if (string.IsNullOrEmpty(productName))  return this;
        this.ProductName = productName;
        return this;
    }
    public Product SetBrandName(string brandName)
    {
        if (string.IsNullOrEmpty(brandName))  return this;
        this.BrandName = brandName;
        return this;
    }
    
    public Product SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description))  return this;
        this.Description = description;
        return this;
    }
    
    public Product SetSize(string size)
    {
        if (string.IsNullOrEmpty(size))  return this;
        this.Sizes = size;
        return this;
    }
    public Product SetColour(string colour)
    {
        if (string.IsNullOrEmpty(colour))  return this;
        this.Colours = colour;
        return this;
    }
    public Product SetSellingPrice(double sellingPrice)
    {
        if (sellingPrice < 1)  return this;
        this.SellingPrice = sellingPrice;
        return this;
    }
    public Product SetBuyingPrice(double buyingPrice)
    {
        if (buyingPrice < 1)  return this;
        this.BuyingPrice = buyingPrice;
        return this;
    }
    public Product SetUnitOfMeasureId(int unitOfMeasureId)
    {
        this.UnitOfMeasureId = unitOfMeasureId;
        return this;
    }

    public Product SetImageByte(byte[]? image)
    {
        if (image == null)  return this;
        this.ProductImage = image;
        return this;
    }
    public Product SetImageLength(long imageLenght)
    {
        if (imageLenght < 1)  return this;
        this.ProductImageLength = imageLenght;
        return this;
    }
    public Product SetImageExtension(string imageExtension)
    {
        if (string.IsNullOrEmpty(imageExtension))  return this;
        this.ProductImageExtension = imageExtension;
        return this;
    }
    
    public Product SetBranchId(int branchId)
    {
        if (branchId < 1)  return this;
        this.BranchId = branchId;
        return this;
    }
    
    public Product SetCurrencyId(int currencyId)
    {
        if (currencyId < 1)  return this;
        this.CurrencyId = currencyId;
        return this;
    }
    
    private static string GenerateProductCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            return 0001.ToString();
        }
        int productCode = int.Parse(code);
        productCode++;
        return productCode.ToString(); 
    }
}