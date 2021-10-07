using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using StoreBL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBL _bl;

        public HomeController(ILogger<HomeController> logger, IBL bl)
        {
            _logger = logger;
            _bl = bl;
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

        [HttpGet("Login")]
        public ActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVM model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string name = model.Name;
                    var result = _bl.GetCustomer(name);
                    if (result != null)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim("username", name));
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                        return Redirect(returnUrl);
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt. No such user found. Please try again.");
                    
                }
                return View("login");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Customer/Create
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View("CreateUser");
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(LoginVM customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkIfUserExist = _bl.SearchCustomer(customer.Name);
                    if (checkIfUserExist == null)
                    {
                        _bl.AddCustomer(customer.ToModel());
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "This name is already in use. Please choose a different name.");
                }
                return View(customer);
            }
            catch
            {
                return View("Index");
            }
        }
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
