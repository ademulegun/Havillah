namespace HavillahWebUI_MVC.Models.Product;

public class IndexViewModel
{
    public ProductsRoot? ProductsRoot { get; set; } = new ProductsRoot();
    public ProductRoot? ProductRoot { get; set; } = new ProductRoot();
    public HavillahWebUI_MVC.Models.Product.AddProductDto AddProductDto { get; set; }

    public HavillahWebUI_MVC.Models.Product.ProductViewModelDto Product { get; set; }
        = new HavillahWebUI_MVC.Models.Product.ProductViewModelDto();
}