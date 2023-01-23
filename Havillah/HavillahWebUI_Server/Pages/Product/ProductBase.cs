using Havillah.Shared.Category;
using HavillahWebUI_MVC.Models.Category;
using HavillahWebUI_Server.Data;
using HavillahWebUI_Server.Helpers;
using HavillahWebUI_Server.Models.Product;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace HavillahWebUI_Server.Pages.Product;

public class ProductBase: ComponentBase
{
    [Parameter]public  string Id { get; set; }
    protected ProductRoot? Product { get; set; } = new();
    protected bool ShowPopup { get; set; } = false;
    [Parameter]public ProductRoot? EditProduct { get; set; } = new();
    protected readonly AddProductDto AddProductDto = new();
    protected readonly AddCategoryDto AddCategoryDto = new();
    [Inject]protected ProductService _productService { get; set; }
    public ProductsRoot? products { get; set; } = new();
    IBrowserFile? selectedFiles;
    [Inject] private IJSRuntime JsRuntime { get; set; }
    public bool IsProducsLoading { get; set; } = true;
    [Parameter]public bool IsCategoryAddButtonDisabled { get; set; } = false;
    public List<GetCategories>? CategoryList { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadProducts();
        }
    }
    
    private async Task LoadProducts()
    {
        StateHasChanged();
        var result = await _productService.GetCategories();
        CategoryList = result?.value;
        products = await _productService.GetProducts();
        foreach (var p in products.value)
        {
            var imageBase64Data = p.productImage;
            var imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            p.image = imageDataUrl;
        }
        StateHasChanged();
        IsProducsLoading = false;
    }
    
    public async Task LoadProduct(string id)
    {
        StateHasChanged();
        this.Product = await _productService.GetProduct(Guid.Parse(this.Id));
        var imageBase64Data = Product.value.productImage;
        var imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
        Product.value.image = imageDataUrl;
        StateHasChanged();
    }

    protected void GetProductToEdit(ProductRoot productRoot)
    {
        if (EditProduct == null) return;
        EditProduct.value.productName = productRoot.value.productName;
        EditProduct.value.id = productRoot.value.id;
        EditProduct.value.brandName = productRoot.value.brandName;
        EditProduct.value.description = productRoot.value.description;
        EditProduct.value.defaultBuyingPrice = productRoot.value.defaultBuyingPrice;
        EditProduct.value.defaultSellingPrice = productRoot.value.defaultSellingPrice;
        EditProduct.value.quantity = productRoot.value.quantity;
        EditProduct.value.sizes = productRoot.value.sizes;
        EditProduct.value.colours = productRoot.value.colours;
    }
    protected async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        selectedFiles = e.GetMultipleFiles().FirstOrDefault();
        if (selectedFiles == null)
        {
            await JsRuntime.ToastrSuccess("Please select a file to upload");
            StateHasChanged();
            return;
        }
        StateHasChanged();
        await JsRuntime.ToastrSuccess("File selected successfully");
    }
    protected async Task UpdateProduct()
    {
        try
        {
            if (selectedFiles == null)
            {
                var response = await _productService.UpdateProducts(new ProductToEditViewModel()
                {
                    Id = EditProduct.value.id, ProductName = EditProduct.value.productName, BrandName = EditProduct.value.brandName,
                    Description = EditProduct.value.description, DefaultBuyingPrice = EditProduct.value.defaultBuyingPrice, 
                    DefaultSellingPrice = EditProduct.value.defaultSellingPrice, Quantity = EditProduct.value.quantity, Sizes = EditProduct.value.sizes, 
                    Colours = EditProduct.value.colours
                });
                this.StateHasChanged();
            }
            else
            {
                Stream stream = selectedFiles.OpenReadStream();
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms, new CancellationToken());
                ms.Close();
                await ms.DisposeAsync();
                var response = await _productService.UpdateProducts(new ProductToEditViewModel()
                {
                    Id = EditProduct.value.id, ProductName = EditProduct.value.productName, BrandName = EditProduct.value.brandName,
                    Description = EditProduct.value.description, DefaultBuyingPrice = EditProduct.value.defaultBuyingPrice, 
                    DefaultSellingPrice = EditProduct.value.defaultSellingPrice, Quantity = EditProduct.value.quantity, Sizes = EditProduct.value.sizes, 
                    Colours = EditProduct.value.colours, ProductImage = ms.ToArray(), ProductImageExtension = Path.GetExtension(selectedFiles.Name), 
                    ProductImageLength = selectedFiles.Size
                });
                this.StateHasChanged();
                if (response.isSuccess) await JsRuntime.ToastrSuccess(response.value);
                await JsRuntime.ToastrFailed(response.error);
            }
        }
        catch (Exception e)
        {
            await JsRuntime.ToastrFailed("An error occured");
            this.StateHasChanged();
        }
    }
    protected async Task AddProduct()
    {
        try
        {
            if (selectedFiles == null)
            {
                await JsRuntime.ToastrWarning("Please take the necessary action");
            }
            else
            {
                Stream stream = selectedFiles.OpenReadStream();
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms, new CancellationToken());
                ms.Close();
                await ms.DisposeAsync();
                var response = await _productService.AddProducts(new Havillah.Shared.Product.AddProductDto()
                {
                    ProductName = AddProductDto.ProductName, BrandName = AddProductDto.BrandName,
                    Description = AddProductDto.Description, DefaultBuyingPrice = AddProductDto.DefaultBuyingPrice, 
                    DefaultSellingPrice = AddProductDto.DefaultSellingPrice, Quantity = AddProductDto.Quantity, Sizes = AddProductDto.Sizes, 
                    Colours = AddProductDto.Colours, ProductImage = ms.ToArray(), ProductImageExtension = Path.GetExtension(selectedFiles.Name), 
                    ProductImageLength = selectedFiles.Size, CategoryId = AddProductDto.CategoryId
                });
                this.StateHasChanged();
                if (response.isSuccess)
                {
                    await JsRuntime.ToastrSuccess(response.value);
                }
                else
                {
                    await JsRuntime.ToastrFailed(response.error);   
                }
            }
        }
        catch (Exception e)
        {
            await JsRuntime.ToastrFailed("An error occured");
            this.StateHasChanged();
        }
    }

    protected async Task AddCategory()
    {
        IsCategoryAddButtonDisabled = true;
        await Task.Delay(4000);
        var response = await _productService.AddCategory(new ProductCategory()
        {
            Name = AddCategoryDto.Name
        });
        if (response.isSuccess)
        {
            await JsRuntime.ToastrSuccess(response.value);
            this.StateHasChanged();
        }
        else
        {
            await JsRuntime.ToastrFailed(response.error);
            this.StateHasChanged();
        }

        IsCategoryAddButtonDisabled = false;
    }

    protected async Task LoadCategories()
    {
        var result = await _productService.GetCategories();
        CategoryList = result?.value;
    }
}