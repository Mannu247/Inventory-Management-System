using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SaleController : Controller
    {
        Inventory_managementEntities db = new Inventory_managementEntities();
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DisplaySale()
        {
            List<Sale> list = db.Sales.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult SaleProduct()
        {
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View();
        }

        [HttpPost]
        public ActionResult SaleProduct(Sale S)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(S);
                db.SaveChanges();
                return RedirectToAction("DisplaySale");
            }
            List<string> list = db.Sales.Select(x => x.Sale_prod).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View(S);
        }
        [HttpGet]
        public ActionResult SaleEdit(int id)
        {
            Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
            List<string> list = db.Products.Select(x => x.Product_name).ToList();
            ViewBag.ProductName = new SelectList(list);
            return View(s);
        }
        [HttpPost]
        public ActionResult SaleEdit(int id, Sale sal)
        {
            Sale sa = db.Sales.Where(x => x.id == id).SingleOrDefault();
            sa.Sale_date = sal.Sale_date;
            sa.Sale_prod = sal.Sale_prod;
            sa.Sale_qnty = sal.Sale_qnty;
            db.SaveChanges();
            return RedirectToAction("DisplaySale");
        }
        [HttpGet]
        public ActionResult SaleDetail(int id) 
        {
            Sale s = db.Sales.Where(x => x.id == id).SingleOrDefault();
            return View(s);
        }
        [HttpGet]
        public ActionResult SaleDelete(int id)
        {
            Sale sa = db.Sales.Where(x => x.id == id).SingleOrDefault();

            if (sa == null)
            {
                ViewBag.Message = "Sale not found";
                return View("Error"); 
            }
            return View(sa);
        }

        [HttpPost]
        public ActionResult SaleDelete(int id, Sale sal)
        {
            Sale saleToDelete = db.Sales.Where(x => x.id == id).SingleOrDefault();

            if (saleToDelete == null)
            {
                ViewBag.Message = "Sale not found";
                return View("Error"); 
            }

            db.Sales.Remove(saleToDelete);
            db.SaveChanges();
            return RedirectToAction("DisplaySale"); 
        }
    }
}
