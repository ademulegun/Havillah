namespace HavillahWebUI_MVC.Models.Product;

public class IndexViewModel
{
    public ProductsRoot? ProductsRoot { get; set; }
    public ProductRoot? ProductRoot { get; set; }
    public HavillahWebUI_MVC.Models.Product.AddProductDto AddProductDto { get; set; }
    public HavillahWebUI_MVC.Models.Product.ProductViewModelDto Product { get; set; }
}