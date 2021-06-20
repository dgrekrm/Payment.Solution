using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Solution.BusinessLayer;
using Payment.Solution.ViewModels;
using Payment.Solution.WebApplication.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Payment.Solution.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthenticationManager _authenticationManager;

        public HomeController(ILogger<HomeController> logger, AuthenticationManager authenticationManager)
        {
            _logger = logger;
            _authenticationManager = authenticationManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _authenticationManager.Login(loginViewModel);

            if (user == null)
            {
                return Json(new { status = 0, message = "Identity number or password is not correct." });
            }

            HttpContext.Session.Set("IdentityNumber", JsonSerializer.SerializeToUtf8Bytes(user.IdentityNumber));
            HttpContext.Session.Set("UserType", JsonSerializer.SerializeToUtf8Bytes(user.UserType));
            HttpContext.Session.TryGetValue("IdentityNumber", out byte[] identityNumber);

            return Json(new { status = 1 });
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.TryGetValue("IdentityNumber", out byte[] _)) return RedirectToAction("Index");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}
