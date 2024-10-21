using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Test.Models;
using Test.Services;
using Test.Utils;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor contextAccessor;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            this.configuration = configuration;
            this.contextAccessor = contextAccessor;
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
                contextAccessor.HttpContext!.Session.SetString("UserData",JsonSerializer.Serialize(new LoginService(configuration).Login(user)));
                return View();
            }
            catch (Exception ex) 
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public IActionResult Logout() 
        {
            contextAccessor.HttpContext!.Session.Remove("UserData");
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
