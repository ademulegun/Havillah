using System.ComponentModel.DataAnnotations;

namespace HavillahWebUI_Server.Models.Product;

public class AddProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string Barcode { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    public int UnitOfMeasureId { get; set; }
    [Required(ErrorMessage = "Buying price is required")]
    public double DefaultBuyingPrice { get; set; } = 0.0;
    [Required(ErrorMessage = "Selling price is required")]
    public double DefaultSellingPrice { get; set; } = 0.0;
    public int BranchId { get; set; }
    public int CurrencyId { get; set; }
    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
    public byte[] ProductImage { get; set; }
    public long ProductImageLength { get; set; }
    public string ProductImageExtension { get; set; }
    [Required(ErrorMessage = "Brand name is required")]
    public string BrandName { get; set; }
    [Required(ErrorMessage = "Sizes is required")]
    public string Sizes { get; set; }
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Colours is required")]
    public string Colours { get; set; }
}