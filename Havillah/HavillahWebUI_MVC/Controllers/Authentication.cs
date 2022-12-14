using HavillahWebUI_MVC.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace HavillahWebUI_MVC.Controllers;

public class Authentication : Controller
{
    private readonly ILogger<Authentication> _logger;

    public Authentication(ILogger<Authentication> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(LoginDto login)
    {
        return View();
    }
}