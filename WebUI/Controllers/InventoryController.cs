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
    public class InventoryController : Controller
    {
        private readonly IBL _bl;
        public InventoryController(IBL bl)
        {
            _bl = bl;
        }
        // GET: InventoryController
        /// <summary>
        /// Get all Inventory of a Store
        /// </summary>
        /// <param name="id">store Id</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            StoreVM store = new StoreVM(_bl.GetStoreById(id));
            return View(store);
        }

        // GET: InventoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InventoryController/Create
        public ActionResult Create(string storeId)
        {
            int id = int.Parse(storeId);
            ViewBag.Store = _bl.GetStoreById(id);
            //ViewData["Store"] = _bl.GetStoreById(id);
            
            return View(new Inventory(id));
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory inventory, int storeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    
                    _bl.AddInventory(inventory);

                    return RedirectToAction(nameof(Index), new {id = inventory.StoreID});

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InventoryController/Edit/5
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

        // GET: InventoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InventoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
