//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace WebUI.Controllers
//{
//    public class AdminController : Controller
//    {
//        // GET: HomeController1
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // GET: HomeController1/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: HomeController1/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: HomeController1/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: HomeController1/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: HomeController1/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: HomeController1/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: HomeController1/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        public ActionResult CreateProduct()
//        {
//            return View();
//        }

//        // POST: ProductController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult CreateProduct(Product product)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    _bl.AddProduct(product);
//                    return RedirectToAction(nameof(Index));
//                }

//                return View();
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
