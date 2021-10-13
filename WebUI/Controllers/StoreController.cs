using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreBL;
using Models;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class StoreController : Controller
    {
        private readonly IBL _bl;
        public StoreController(IBL bl)
        {
            _bl = bl;
        }
        // GET: StoreController
        public ActionResult Index()
        {
            List<StoreVM> allStores = _bl.GetAllStores().Select(r => new StoreVM(r)).ToList();
            //allStores.OrderBy(s => s.Name);
            return View(allStores);
        }

        // GET: StoreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreVM store)
        {
            try
            {
                //the data in my form is valid
                if (ModelState.IsValid)
                {
                    _bl.AddStore(store.ToModel());
                    return RedirectToRoute(nameof(HomeController));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_bl.GetStoreById(id));
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StoreFront store)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateStore(store);
                    return RedirectToAction(nameof(Index));

                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new StoreVM(_bl.GetStoreById(id)));

        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _bl.DeleteStore(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet("LocationOrders")]
        public ActionResult LocationOrders(int id)
        {
            List<Order> orders = _bl.GetAllOrders();
            List<Order> orderByLocations = new List<Order>();
            foreach(var order in orders)
            {
                if(order.StoreFrontId == id)
                {
                    orderByLocations.Add(order);
                }
            }

            return View(orderByLocations);
        }
    }
}
