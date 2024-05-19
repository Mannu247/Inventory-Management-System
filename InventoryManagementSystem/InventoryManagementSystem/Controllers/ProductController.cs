using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        Inventory_managementEntities db = new Inventory_managementEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DisplayProduct()
        {
            List<Product> list = db.Products.OrderByDescending(x => x.id).ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product pro)
        {
            db.Products.Add(pro);
            db.SaveChanges();
            return RedirectToAction("DisplayProduct");
        }
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            Product pr = db.Products.Where(x => x.id == id).SingleOrDefault();
            return View(pr);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id, Product pro)
        {
            Product pr = db.Products.Where(x => x.id == id).SingleOrDefault();
            pr.Product_name = pro.Product_name;
            pr.Product_qnty = pro.Product_qnty;
            db.SaveChanges();
            return RedirectToAction("DisplayProduct");
        }
        [HttpGet]
        public ActionResult ProductDetail(int id)
        {
            Product pro = db.Products.Where(x => x.id == id).SingleOrDefault();
            return View(pro);
        }
        [HttpGet]
        public ActionResult ProductDelete(int id)
        {
            // Fetch the product with the specified ID
            Product pro = db.Products.Where(x => x.id == id).SingleOrDefault();

            // If the product is not found, handle it appropriately
            if (pro == null)
            {
                ViewBag.Message = "Product not found";
                return View("Error"); // Assuming you have an error view to display the message
            }

            // Return the delete confirmation view with the product details
            return View(pro);
        }

        [HttpPost]
        public ActionResult ProductDelete(int id, Product pro)
        {
            // Fetch the product with the specified ID again
            Product productToDelete = db.Products.Where(x => x.id == id).SingleOrDefault();

            // Check if the product exists
            if (productToDelete == null)
            {
                ViewBag.Message = "Product not found";
                return View("Error"); // Assuming you have an error view to display the message
            }

            // Remove the product from the database
            db.Products.Remove(productToDelete);
            db.SaveChanges();

            // Redirect to a different view or display a success message
            return RedirectToAction("DisplayProduct"); // Redirect to the action that displays the product list
        }
    }
}