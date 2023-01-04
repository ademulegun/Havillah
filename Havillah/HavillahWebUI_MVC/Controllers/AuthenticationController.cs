using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Havillah.Shared;
using HavillahWebUI_MVC.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using LoginDto = HavillahWebUI_MVC.Models.Authentication.LoginDto;

namespace HavillahWebUI_MVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration configuration)
    {
        _client = new HttpClient();
        _configuration = configuration;
        _client.BaseAddress = new Uri(_configuration.GetSection("Jwt").GetSection("Issuer").Value);
        _logger = logger;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var body = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");
        var httpResult = await _client.PostAsync("security/generateToken", body);
        if (httpResult.IsSuccessStatusCode)
        {
            var res = await httpResult.Content.ReadAsStringAsync();
            var bodyResponse = JsonSerializer.Deserialize<TokenResponse>(res);
            if (!string.IsNullOrEmpty(bodyResponse?.value?.tokenValue))
            {
                SetJWTCookie(bodyResponse.value.tokenValue);
                HttpContext.Session.SetString("Token", bodyResponse.value.tokenValue);
                return RedirectToAction("Index", "Product");   
            }
        }
        return View(login);
    }
    
    private void SetJWTCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddHours(3),
        };
        Response.Cookies.Append("jwtCookie", token, cookieOptions);
    }
}