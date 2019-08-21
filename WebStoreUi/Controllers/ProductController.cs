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
    public class ProductController : Controller
    {
        //dependency injection
        private IStoreRepository repository; //1 зависимость
        private int PageSize = 5;
        public ProductController()
        {
            repository = new StoreRepository();//2 зависимость
        }

        // GET: Product
        public ActionResult List(int page = 1)
        {
            var plvm = new ProductListViewModel
            {
                Products = repository
                            .Products
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = repository.Products.Count(),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                }
            };

            return View(plvm);
        }


        public async Task<ActionResult> Details(int? id)
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
        public async Task<ActionResult> Delete(Product p)
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
        public async Task<ActionResult> Delete(int id)
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
        public ActionResult Add()
        {
            SelectList categ = new SelectList(repository.Categories, "Id", "Name");
            ViewBag.Categ = categ;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Product p)
        {
            ((DbSet<Product>)repository.Products).Add(p);
            await ((StoreRepository)repository).Context.SaveChangesAsync();

            //return View();
            return RedirectToAction("List");

        }



        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
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
        public async Task<ActionResult> Edit(Product p)
        {
            ((StoreRepository)repository).Context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            await ((StoreRepository)repository).Context.SaveChangesAsync();
            return RedirectToAction("List");
        }








        //// GET: Product
        //public ActionResult List( )
        //{
        //    return View(repository.Products);
        ////    var plvm = new ProductListViewModel() {



        //    //    Products = repository.Products.OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize),
        //    //    PagingInfo = new PagingInfo() {

        //    //        CurrectPage = page, 
        //    //         TotalItems = PageSize,
        //    //        ItemsPerPage = repository.Products.Count()
        //    //    }


        //    //    // return View(repository.Products);
        //    //    //ViewBag.Count = repository.Products.Count();
        //    //    //ViewBag.PageSize = PageSize;
        //    //    //ViewBag.CurrentPage = page;
        //    //   // return View(products);


        //    //};

        //    ////////////var products = repository.Products.OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize);
        //    ////////////// return View(repository.Products);
        //    ////////////ViewBag.Count = repository.Products.Count();
        //    ////////////ViewBag.PageSize = PageSize;
        //    ////////////ViewBag.CurrentPage = page;
        //    //////////// products
        //    // return View(plvm);

        //}

    }
}