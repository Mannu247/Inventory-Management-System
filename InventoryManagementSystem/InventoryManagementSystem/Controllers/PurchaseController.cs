using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class PurchaseController : Controller
    {
        Inventory_managementEntities db = new Inventory_managementEntities();

        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplayPurchase()
        {
            List<Purchase> list = db.Purchases.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult PurchaseProduct()
        {
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View();
        }

        [HttpPost]
        public ActionResult PurchaseProduct(Purchase pur)
        {
            if (ModelState.IsValid)
            {
                db.Purchases.Add(pur);
                db.SaveChanges();
                return RedirectToAction("DisplayPurchase");
            }
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View(pur);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(int id, Purchase pur)
        {
            Purchase p = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            p.Purchase_date = pur.Purchase_date;
            p.Purchase_prod = pur.Purchase_prod;
            p.Purchase_qnty = pur.Purchase_qnty;
            db.SaveChanges();
            return RedirectToAction("DisplayPurchase");
        }
        [HttpGet]
        public ActionResult PurchaseDetail(int id)
        {
            Purchase pro = db.Purchases.Where(x => x.id == id).SingleOrDefault();
            return View(pro);
        }
        [HttpGet]
        public ActionResult PurchaseDelete(int id)
        {
            // Fetch the purchase with the specified ID
            Purchase pro = db.Purchases.Where(x => x.id == id).SingleOrDefault();

            // If the product is not found, handle it appropriately
            if (pro == null)
            {
                ViewBag.Message = "Purchase not found";
                return View("Error"); // Assuming you have an error view to display the message
            }

            // Return the delete confirmation view with the purchase details
            return View(pro);
        }

        [HttpPost]
        public ActionResult PurchaseDelete(int id, Purchase pro)
        {
            // Fetch the purchase with the specified ID again
            Purchase purchaseToDelete = db.Purchases.Where(x => x.id == id).SingleOrDefault();

            // Check if the purchase exists
            if (purchaseToDelete == null)
            {
                ViewBag.Message = "Purchase not found";
                return View("Error"); // Assuming you have an error view to display the message
            }

            // Remove the purchase from the database
            db.Purchases.Remove(purchaseToDelete);
            db.SaveChanges();

            // Redirect to a different view or display a success message
            return RedirectToAction("DisplayPurchase"); // Redirect to the action that displays the product list
        }
    }
}