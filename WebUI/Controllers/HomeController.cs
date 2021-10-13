using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Serilog;
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

        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            Customer customer = _bl.GetCustomer(Request.Cookies["CurrentCustomer"]);
            
            return View("Profile", customer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("Login")]
        public ActionResult Login()
        {
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
                        return RedirectToAction("Index", "Store");
                    }
                    var result = _bl.GetCustomer(name);
                    if (result != null)
                    {
                        Response.Cookies.Append("CustomerId", result.Id.ToString());
                        Response.Cookies.Append("CurrentCustomer", name);

                        return View(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt. No such user found. Please try again.");
                    Log.Information("Someone tried to login with wrong information.");
                }
                return View("login");
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: Customer/Create
        public ActionResult CreateUser()
        {
            return View();
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
                        Log.Information("Account successfully Created.");
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
            Log.Information("Logged out successfully.");
            return RedirectToAction("Index");
        }

        [HttpGet("Stores")]
        public ActionResult Stores()
        {
            List<StoreVM> allStores = _bl.GetAllStores().Select(r => new StoreVM(r)).ToList();
            return View(allStores);
        }

        [HttpGet("Inventories")]
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
        [HttpGet]
        public ActionResult StartOrder()
        {
            int storeNumber = int.Parse(Request.Cookies["StoreId"]);
            List<Inventory> inventories = _bl.GetInventoriesByStoreId(storeNumber);
            foreach (var inventory in inventories)
            {
                inventory.Product = _bl.GetProductById(inventory.ProductID);
            }
            ViewBag.Inventory = inventories;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartOrder(IFormCollection form)
        {
            int customerNumber = int.Parse(Request.Cookies["CustomerId"]);
            int storeNumber = int.Parse(Request.Cookies["StoreId"]);
            Order order = new Order();
            order.StoreFrontId = storeNumber;
            order.CustomerId = customerNumber;
            order.OrderDate = DateTime.Now;
            order = _bl.AddOrder(order);
            int totalItemBought = 0;
            foreach (var key in form)
            {
                if (key.Key == "__RequestVerificationToken")
                {
                    break;
                }
                if (key.Value == 0)
                {
                    continue;
                }


                int howMany = Int32.Parse(key.Value);

                int productId = int.Parse(key.Key);

                LineItems item = new LineItems();
                item.ProductId = productId;
                item.OrderId = order.Id;
                item.Quantity = howMany;
                List<Inventory> inventories = _bl.GetInventoriesByStoreId(storeNumber);

                foreach (var inv in inventories)
                {
                    if (inv.ProductID == productId)
                    {
                        inv.Quantity -= howMany;
                    }

                    _bl.UpdateInventory(inv);

                }

                order.LineItems.Add(item);
                Product product = _bl.GetProductById(productId);
                order.Total += howMany * product.Price;
                totalItemBought += howMany;
            }
            
            _bl.UpdateOrder(order);
            ViewBag.ItemsBought = totalItemBought;
            ViewBag.Order = order;
            Log.Information("Order created successfully.");
            return View("OrderSuccessful");
        }
        public ActionResult OrderSuccessful()
        {
            return View();

        }

        [HttpGet("OrderByLatest")]
        public ActionResult OrderByLatest()
        {
            Customer customer = _bl.GetCustomer(Request.Cookies["CurrentCustomer"]);
            List<Order> orders = customer.Orders;
            orders = orders.OrderByDescending(o => o.OrderDate).ToList();
            return View(orders);
        }
        [HttpGet("OrderByOldest")]
        public ActionResult OrderByOldest()
        {
            Customer customer = _bl.GetCustomer(Request.Cookies["CurrentCustomer"]);
            List<Order> orders = customer.Orders;
            orders = orders.OrderBy(o => o.OrderDate).ToList();
            return View(orders);
        }
    }

}

