namespace Havillah.ApplicationServices.Product.AddProduct.Dto;

public class AddProductDto
{
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