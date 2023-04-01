// Title       :Online Bus Booking System
//  Author      :Abinash(IFET)
// Created at  :01-03-2023
// Updated at  :13-03-2023
// Reviewed by :
// Reviewed at :




using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using FirstApp.Filters;

namespace FirstApp.Controllers;
[ExceptionLogFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> logOut()
    {  
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Console.WriteLine(User.Identity.IsAuthenticated);
        return RedirectToAction("Index","Home");
    }
    public IActionResult map()
    {
         return Redirect("https://www.google.com/maps/dir//Aspire+Systems,+SIPCOT+IT+Park,+1%2FD-1,+First+Main+Road,+Siruseri,+Tamil+Nadu+603103/@12.9284151,80.1754389,12z/data=!4m8!4m7!1m0!1m5!1m1!1s0x3a525a5d709633d1:0xd457a8c2f985c180!2m2!1d80.2235385!2d12.8303318");;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
