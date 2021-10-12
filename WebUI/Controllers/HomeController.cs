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

        public List<LineItems> itemsCart = new List<LineItems>();

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

        public IActionResult Profile()
        {
            return View("Profile");
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
        public ActionResult Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string name = model.Name;
                    if (name == "Admin")
                    {
                        Response.Cookies.Append("Admin", "Admin");
                        return RedirectToAction(nameof(Index));
                    }
                    var result = _bl.GetCustomer(name);
                    if (result != null)
                    {
                        Response.Cookies.Append("CustomerId", result.Id.ToString());
                        Response.Cookies.Append("CurrentCustomer", name);

                        return View("Profile");
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
                    var checkIfUserExist = _bl.GetCustomer(customer.Name);
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
        public ActionResult Logout()
        {
            if (Request.Cookies["CurrentCustomer"] != null)
            {
                Response.Cookies.Delete("CurrentCustomer");
                Response.Cookies.Delete("CustomerId");
                Response.Cookies.Delete("StoreId");
            }
            if (Request.Cookies["Admin"] != null)
            {
                Response.Cookies.Delete("Admin");
                Response.Cookies.Delete("StoreId");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Stores()
        {
            List<StoreVM> allStores = _bl.GetAllStores().Select(r => new StoreVM(r)).ToList();
            return View(allStores);
        }

        public ActionResult Inventories(int storeId)
        {
            Response.Cookies.Delete("StoreId");
            Response.Cookies.Append("StoreId", storeId.ToString());
            List<Inventory> inventoriesByStoreId = _bl.GetInventoriesByStoreId(storeId);
            foreach (var inventory in inventoriesByStoreId)
            {
                inventory.Product = _bl.GetProductById(inventory.ProductID);
            }

            ViewBag.Inventory = inventoriesByStoreId;
            return View(inventoriesByStoreId);
        }

        public ActionResult StartOrder(int productId)
        {

            int storeNumber = int.Parse(Request.Cookies["StoreId"]);
            int customerNumber = int.Parse(Request.Cookies["CustomerId"]);

            Order order = new Order();
            order.StoreFrontId = storeNumber;
            order.CustomerId = customerNumber;
            order.OrderDate = DateTime.Now;
            _bl.AddOrder(order);

            List<Inventory> inventories = _bl.GetInventoriesByStoreId(storeNumber);
            foreach (var inventory in inventories)
            {
                inventory.Product = _bl.GetProductById(inventory.ProductID);
            }
            ViewBag.Inventory = inventories;

            return View();
        }
       
        [HttpPost]
        public ActionResult LineItem(List<LineItems> items)
        {

            return View();

        }
    }

}

