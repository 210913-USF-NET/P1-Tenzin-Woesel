using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using StoreBL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string name = model.Name;
                    if(name == "")
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt. Please try again.");
                        return View("Index");
                    }
                    var result = _bl.GetCustomer(name);
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Privacy));
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt. Please try again.");
                }
                return View(model);
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
    }
}
