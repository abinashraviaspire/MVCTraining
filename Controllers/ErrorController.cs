using Microsoft.AspNetCore.Mvc;
namespace FirstApp.Controllers{
    public class ErrorController : Controller{
        public IActionResult Index(){
            return View();
        }
    }
}