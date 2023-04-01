// Title       :Online Bus Booking System
//  Author     :Abinash(IFET)
// Created at  :01-03-2023
// Updated at  :13-03-2023
// Reviewed by :
// Reviewed at :

using Microsoft.AspNetCore.Mvc;
using FirstApp.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
namespace FirstApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
{
    public IActionResult Index(Employee employee)
    {
        try
        {
        HttpContext.Session.SetString("UserName",CurrentUser.display(employee.EmailId));
        return View();
        }
        catch (Exception)
        {    
            return View();
        }
    }

    public ActionResult showProfile(){
        return View();
    }
    [HttpGet]
    public ActionResult bookRide(){
        return View();
    }
    [HttpPost]
    public ActionResult bookRide(CurrentUser currentUser){
      
        string? status=CurrentUser.bookBus(currentUser.Destination,currentUser.Date);
        return View();
    }
    
    public ActionResult seatBooked(){
        return View();
    }

    public ActionResult contact(){
        return View();
    }
    
   
    
}
}