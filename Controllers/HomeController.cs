using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdAndCat.Contexts;
using ProdAndCat.Models;

namespace ProdAndCat.Controllers
{
    public class HomeController : Controller
    {
        private HomeContext dbContext;

        public HomeController(HomeContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/products")]
        public IActionResult Products()
        {
            ViewBag.Products = dbContext.Products.OrderBy(p => p.CreatedAt).ToList();
            return View();
        }

        [HttpPost("/process")]
        public IActionResult AddProduct(Product NewProduct)
        {
            dbContext.Products.Add(NewProduct);
            dbContext.SaveChanges();
            return Redirect("/products");
        }

        [HttpGet("/categories")]

        public IActionResult Categories()
        {
            ViewBag.Categories = dbContext.Categories.OrderBy(c => c.CreatedAt).ToList();
            return View();
        }

        [HttpPost("/newcategory")]
        public IActionResult NewCategory(Category NewCategory)
        {
            dbContext.Categories.Add(NewCategory);
            dbContext.SaveChanges();
            return Redirect("/categories");
        }

        [HttpGet("/products/{ProductId}")]

        public IActionResult DisplayProduct(int productId)
        {
            ViewBag.ProdWithCats = dbContext.Products.Include(p => p.AssocCategories).ThenInclude(a => a.Category).FirstOrDefault(p => p.ProductId == productId);
            ViewBag.NotOnProd = dbContext.Categories.Include(c => c.AssocProducts).Where(c => c.AssocProducts.All(a => a.ProductId != productId));
            return View("DisplayProduct");
        }

        [HttpGet("/categories/{CategoryId}")]
        public IActionResult DisplayCategory(int categoryId)
        {
            ViewBag.Category = dbContext.Categories.Include(p => p.AssocProducts).ThenInclude(a => a.Product).FirstOrDefault(p => p.CategoryId == categoryId);
            ViewBag.NotOnCat = dbContext.Products.Include(c => c.AssocCategories).Where(c => c.AssocCategories.All(a => a.CategoryId != categoryId)).ToList();
            return View("DisplayCategory");
        }

        [HttpPost("process/{path}")]

        public IActionResult Process(Association newAssoc, string path)
        {
            dbContext.Associations.Add(newAssoc);
            dbContext.SaveChanges();
            if(path == "category")
            {
                return Redirect($"/Categories/{newAssoc.CategoryId}");
            }
            else if(path == "product")
            {
                return Redirect($"/products/{newAssoc.ProductId}");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }








///////////////////////////////////////

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
