using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Test.Models;
using Test.Services;
using Test.Utils;

namespace Test.Controllers
{
    public class HomeController(IConfiguration configuration, IHttpContextAccessor contextAccessor) : Controller
    {
        private readonly IConfiguration configuration = configuration;
        private readonly IHttpContextAccessor contextAccessor = contextAccessor;

        #region Login Page
        //Index page
        [HttpGet("/")]
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region Login Functionnality
        //Function for login to the home page
        [HttpPost("/")]
        public async Task<IActionResult> Login(string username,string password)
        {
            try
            {
                LoginModel user = new()
                {
                    UserName = username,
                    Password = PasswordEncryption.ToHash(password)
                };
                

                LoginModel logged = await new LoginService(configuration).Login(user)!;

                //Serialize the data retrieved from database and use to the session
                contextAccessor.HttpContext!.Session.SetString("UserData",JsonSerializer.Serialize(logged));
                return RedirectToAction("Home","Home");
            }
            catch (Exception ex) 
            {
                //Display the error got during the process of retrieving data from database
                TempData["error"] = ex.Message;
                return RedirectToAction("Login", "Home");
            }
        }
        #endregion


        [HttpGet]
        public IActionResult Home() 
        {
            try
            {
                string session = contextAccessor.HttpContext!.Session.GetString("UserData")!;
                ViewData["isConnected"] = SessionService.IsConnected(session);
                ViewData["userData"] = SessionService.GetUserData(session);
                return View();
            }
            catch (Exception ex) 
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Login", "Home");
            }
        }



        #region Logout Functionnality
        [HttpGet]
        public IActionResult Logout() 
        {
            TempData["error"] = "You need to connect to use the app";
            //Removing the key of the session that we use to stock our data
            contextAccessor.HttpContext!.Session.Remove("UserData");
            return RedirectToAction("Login", "Home");
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
