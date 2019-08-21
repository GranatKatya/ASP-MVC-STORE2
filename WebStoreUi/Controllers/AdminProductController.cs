using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Abstract;
using WebStoreDomain.Concrete;
using WebStoreDomain.Entities;
using WebStoreUi.Models;

namespace WebStoreUi.Controllers
{
    public class AdminProductController : Controller
    {
        private IStoreRepository repository; //1 зависимость
        private int PageSize = 5;
        public AdminProductController()
        {
            repository = new StoreRepository();//2 зависимость
        }





        // Выводим всех футболистов
        [HttpGet]
        public ActionResult AllProducts()
        {
            var players = ((DbSet<Product>)repository.Products).Include(p => p.Category);
            ViewBag.Product = players;
            return View(players.ToList());
        }



        public ActionResult List(int page = 1)
        {
            var players = ((DbSet<Product>)repository.Products).Include(p => p.Category).OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize);
            var plvm = new ProductListViewModel
            {
                Products= players,
                //Products = repository
                //           .Products
                //           .OrderBy(p => p.Id)
                //           .Skip((page - 1) * PageSize)
                //           .Take(PageSize).Include(p => p.Category),
                PagingInfo = new PagingInfo
                {
                    TotalItems = repository.Products.Count(),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                }
            };

            return View(plvm);

            //// IEnumerable<Product> products = (DbSet<Product>)repository.Products;
            // IEnumerable<Product> products = ((DbSet<Product>)repository.Products).Include(p => p.Category);
            // //ViewBag.Products = products;
            // return View(products);
        }

        public async Task<ActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            Product p = await ((DbSet<Product>)repository.Products).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            SelectList cat = new SelectList(repository.Categories, "Id", "Name", p.CategoryId);
            ViewBag.Categ = cat;
            return View(p);

        }



















        [HttpGet]
        public async Task<ActionResult> DeleteProduct(Product p)
        {
            // Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            Product p1 = await ((DbSet<Product>)repository.Products).FindAsync(p.Id);
            if (p1 == null)
            {
                return HttpNotFound();
            }


            SelectList cat = new SelectList(repository.Categories, "Id", "Name", p.CategoryId);
            ViewBag.Categ = cat;
            return View(p1);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            Product p = await ((DbSet<Product>)repository.Products).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            ((DbSet<Product>)repository.Products).Remove(p);
            // ((DbContext)repository).SaveChanges();
            ((StoreRepository)repository).Context.SaveChanges();

            return RedirectToAction("List");
        }



        [HttpGet]
        public ActionResult CreateProduct()
        {
            SelectList categ = new SelectList(repository.Categories, "Id", "Name");
            ViewBag.Categ = categ;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product p)
        {
            ((DbSet<Product>)repository.Products).Add(p);
            await ((StoreRepository)repository).Context.SaveChangesAsync();

            //return View();
            return RedirectToAction("List");

        }



        [HttpGet]
        public async Task<ActionResult> EditProduct(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            Product p = await ((DbSet<Product>)repository.Products).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            SelectList cat = new SelectList(repository.Categories, "Id", "Name", p.CategoryId);
            ViewBag.Categ = cat;
            return View(p);
        }


        [HttpPost]
        public async Task<ActionResult> EditProduct(Product p)
        {
            ((StoreRepository)repository).Context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            await ((StoreRepository)repository).Context.SaveChangesAsync();
            return RedirectToAction("List");
        }






    }
}