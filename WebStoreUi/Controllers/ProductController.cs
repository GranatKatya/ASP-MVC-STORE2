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
        private IStoreRepository<Product> repository; //1 зависимость
        private IStoreRepository<Category> categrepository; //1 зависимость
        private int PageSize = 5;
        public ProductController()
        {
            repository = new ProductRepository();//2 зависимость
            categrepository =  new CategoryRepository();
        }

        // GET: Product
        public ActionResult List(string category , int page = 1)
        {
            var plvm = new ProductListViewModel
            {
                Products = repository
                            .Items
                            .Where(p=>category == null || p.Category.Name == category)
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = repository.Items.Where(p => category == null || p.Category.Name == category).Count(),
                    // TotalItems = repository.Items.Count(p => category == null || p.Category.Name == "For body"),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentCategory = category
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
            //е удалось привести тип объекта "System.Data.Entity.Infrastructure.DbQuery`1[WebStoreDomain.Entities.Product]" 
            //к типу "System.Data.Entity.DbSet`1[WebStoreDomain.Entities.Product]".
            Product p = await ((DbSet<Product>)repository.Items).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            SelectList cat = new SelectList(categrepository.Items, "Id", "Name", p.CategoryId);
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
            Product p1 = await ((DbSet<Product>)repository.Items).FindAsync(p.Id);
            if (p1 == null)
            {
                return HttpNotFound();
            }


            SelectList cat = new SelectList(categrepository.Items, "Id", "Name", p.CategoryId);
            ViewBag.Categ = cat;
            return View(p1);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            Product p = await ((DbSet<Product>)repository.Items).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            ((DbSet<Product>)repository.Items).Remove(p);
            // ((DbContext)repository).SaveChanges();
           ((ProductRepository)repository).Context.SaveChanges();

            return RedirectToAction("List");
        }



        [HttpGet]
        public ActionResult Add()
        {
            SelectList categ = new SelectList(categrepository.Items, "Id", "Name");
            ViewBag.Categ = categ;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(Product p)
        {
            ((DbSet<Product>)repository.Items).Add(p);
            await ((ProductRepository)repository).Context.SaveChangesAsync();

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
            Product p = await ((DbSet<Product>)repository.Items).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            SelectList cat = new SelectList(categrepository.Items, "Id", "Name", p.CategoryId);
            ViewBag.Categ = cat;
            return View(p);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Product p)
        {
            ((ProductRepository)repository).Context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            await ((ProductRepository)repository).Context.SaveChangesAsync();
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