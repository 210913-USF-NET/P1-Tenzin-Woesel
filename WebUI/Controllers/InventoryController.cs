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
            ViewData["Integer"] = id;

            List<Inventory> inventoryByStore = _bl.GetInventoriesByStoreId(id);

            foreach (var inventory in inventoryByStore)
            {
                inventory.Product = _bl.GetProductById(inventory.ProductID);
            }



            return View(inventoryByStore);
        }

        // GET: InventoryController/Details/5
        [HttpGet("Details")]
        public ActionResult Details(int id)
        {
            Inventory inventoryDetails = _bl.GetInventoryById(id);
            if (inventoryDetails == null)
            {
                ModelState.AddModelError(string.Empty, "No invetories to look at");
                return View();
            }
            Product productById = _bl.GetProductById(inventoryDetails.ProductID);
            inventoryDetails.Product = productById;

            return View(inventoryDetails);
        }

        // GET: InventoryController/Create
        [HttpGet]
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

                    return RedirectToAction(nameof(Index), new { id = inventory.StoreID });

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoryController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(_bl.GetInventoryById(id));
        }

        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inventory inventory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateInventory(inventory);
                    return RedirectToAction("Index", "Store");
                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoryController/Delete/5
        [HttpGet]
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
                _bl.DeleteInventory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
