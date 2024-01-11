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
using FirstApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
{

    private readonly EmployeeDBContext employeeDBContext;

        public DashboardController(EmployeeDBContext employeeDBContext)
        {
            this.employeeDBContext = employeeDBContext;
            }
        [HttpGet]
        public async Task<IActionResult> displayHistory()
        {
            var empolyees = await employeeDBContext.BookingHistories.Where(b=>b.UserId==CurrentUser.UserId).ToListAsync();
            return View(empolyees);
        }



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

    public async Task<IActionResult> showProfile(){
        var empolyees = await employeeDBContext.Employees.Where(b=>b.EmployeeId==CurrentUser.UserId).ToListAsync();
        return View(empolyees);
    }
    [HttpGet]
    public ActionResult bookRide(){
        return View();
    }
    [HttpPost]
    public ActionResult bookRide(CurrentUser currentUser,Employee employee){
      
    string? status=CurrentUser.bookBus(currentUser.Destination,currentUser.Date);
        if(status=="Success"){
            string? to=TempData["mail"].ToString();
           string notification=CurrentUser.sendNotification(to,currentUser);
           
            return RedirectToAction("seatBooked",currentUser);
        }
        else if(status=="Leave")
        {
            
            ModelState.AddModelError(nameof(BookingHistory.Date),"Please Select  Working Days");
            return View();
        }
        else if(status=="No-Destination")
        {
            ViewBag.message="Please Select Available Destinations";
            return View();
        }
        
         ModelState.AddModelError(nameof(BookingHistory.Date),"Please Select Upcomming Days");
            return View();
           
        
    }
    // [HttpGet]
    public ActionResult seatBooked(){
        return View();
    }


    public ActionResult contact(){
        return View();
    }

    [HttpGet]
     public ActionResult sendRequest(){
        return View();
    }
    [HttpPost]
     public ActionResult sendRequest(Request request){
        if (ModelState.IsValid)
        {
        CurrentUser.sendRequest(request.Message);
        return RedirectToAction("Index");   
        }
        return View();
    }
    
        
}
}