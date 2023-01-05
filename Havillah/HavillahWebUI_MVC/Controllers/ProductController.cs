using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> ba8a519 (trying)

namespace HavillahWebUI_MVC.Controllers;

public class Product : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
<<<<<<< HEAD
=======
using System.Diagnostics;
using Havillah.Shared.Product;
using HavillahWebUI_MVC.Models;
using HavillahWebUI_MVC.Models.Authentication;
using HavillahWebUI_MVC.Models.Product;
using HavillahWebUI_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using AddProductDto = HavillahWebUI_MVC.Models.Product.AddProductDto;

namespace HavillahWebUI_MVC.Controllers;

//[Authorize]
public class ProductController : Controller
{
    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        //var jwt = Request.Cookies["jwtCookie"];
        IndexViewModel viewModel = new IndexViewModel();
        string? token = HttpContext.Session.GetString("Token");
        if (token == null)
        {
            return RedirectToAction("Login", "Authentication"); 
        }
        viewModel.ProductsRoot = await _productService.GetProducts();
        foreach (var product in viewModel.ProductsRoot.value)
        {
            string imageBase64Data = product.productImage;
            string imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            product.Image = imageDataUrl;
        }
        return View(viewModel);
    }

    public IActionResult AddProduct()
    {
        return PartialView("_AddProductPartialView");   
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProduct(AddProductDto product)
    {
        IndexViewModel viewModel = new IndexViewModel();
        MemoryStream ms = new MemoryStream();
        await product.ImageFile.CopyToAsync(ms, new CancellationToken());
        ms.Close();
        await ms.DisposeAsync();
        var productResponse = await _productService.AddProducts(new Havillah.Shared.Product.AddProductDto()
        {
            DefaultBuyingPrice = product.DefaultBuyingPrice, DefaultSellingPrice = product.DefaultSellingPrice,
            Description = product.Description, ProductImage = ms.ToArray(), ProductImageExtension = Path.GetExtension(product.ImageFile.FileName),
            ProductImageLength = product.ImageFile.Length, ProductName = product.ProductName, Quantity = product.Quantity, 
            ProductCode = product.ProductCode
        });
        viewModel.ProductsRoot = await _productService.GetProducts();
        return RedirectToAction("Index", viewModel);
    }
    
    //Get
    public IActionResult Product()
    {
        return View("Product");   
    }
    
    [HttpPost]
    public IActionResult Product(ProductDto product)
    {
        return RedirectToAction("Index");   
    }
    
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var model = new IndexViewModel()
        {
            ProductRoot = await _productService.GetProduct(id)
        };
        return PartialView("_ProductPartialView", model.ProductRoot);   
>>>>>>> b18b03624866f944a75dad9ea6f579c65b37869b
=======
>>>>>>> ba8a519 (trying)
    }
}