using Microsoft.AspNetCore.Mvc;

namespace HavillahWebUI_MVC.Controllers;

public class Product : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}