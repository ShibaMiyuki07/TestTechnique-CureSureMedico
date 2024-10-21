using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;
using Test.Utils;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/")]
        public IActionResult Login(string username,string password)
        {
            try
            {
                LoginModel user = new()
                {
                    UserName = username,
                    Password = PasswordEncryption.ToHash(password)
                };
                return View();
            }
            catch (Exception ex) 
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Login", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
