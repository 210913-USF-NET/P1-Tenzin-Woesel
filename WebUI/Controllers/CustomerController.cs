using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IBL _bl;
        public CustomerController(IBL bl)
        {
            _bl = bl;
        }
        // GET: Customer
        public ActionResult Index()
        {
            List<Customer> allCustomers = _bl.GetAllCustomers();
            return View(allCustomers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(string name)
        {
            Customer customerDetails = _bl.GetCustomer(name);
            return View(customerDetails);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.AddCustomer(customer);

                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {  

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string name)
        {
            return View(_bl.GetCustomer(name));
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string name, IFormCollection collection)
        {
            try
            {
                //_bl.DeleteCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult OrderHistory(int id)
        {
            List<Order> orders = _bl.GetAllOrders();
            List<Order> orderByCustomer = new List<Order>();
            foreach (var o in orders)
            {
                if (o.CustomerId == id)
                {
                    orderByCustomer.Add(o);
                }
            }
            return View(orderByCustomer);
        }
    }
}
