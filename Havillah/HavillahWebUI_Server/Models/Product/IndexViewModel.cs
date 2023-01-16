namespace HavillahWebUI_Server.Models.Product;

public class IndexViewModel
{
    public ProductsRoot? ProductsRoot { get; set; } = new ProductsRoot();
    public ProductRoot? ProductRoot { get; set; } = new ProductRoot();
    public AddProductDto AddProductDto { get; set; }

    public ProductViewModelDto Product { get; set; }
        = new ProductViewModelDto();
}