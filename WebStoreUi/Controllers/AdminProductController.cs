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
        private IStoreRepository<Product> repository; //1 зависимость
        private IStoreRepository<Category> categrepository; //1 зависимость
        private int PageSize = 5;
        //public AdminProductController()
        //{
        //    repository = new ProductRepository();//2 зависимость
        //    categrepository = new CategoryRepository();
        //}


        public AdminProductController(IStoreRepository<Product> storeRepository, IStoreRepository<Category> storeRepository2)
        {
            repository = storeRepository; // there is no 2 injection 
            categrepository = storeRepository2;                          // categrepository = new CategoryRepository();
        }



        // Выводим всех футболистов
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AllProducts()
        {
            var players = ((DbSet<Product>)repository.Items).Include(p => p.Category);
            ViewBag.Product = players;
            return View(players.ToList());
        }



        [Authorize(Roles = "Admin")]
        public ActionResult List(string name, int page = 1)
        {
            var players = ((DbSet<Product>)repository.Items).Where(p => name == null ||  p.Name.Contains(name)).Include(p => p.Category).OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize);
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
                    TotalItems = repository.Items.Where(p => name == null || p.Name.Contains(name)).Count(),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentItemSearch = name

            };

            return View(plvm);

            //// IEnumerable<Product> products = (DbSet<Product>)repository.Products;
            // IEnumerable<Product> products = ((DbSet<Product>)repository.Products).Include(p => p.Category);
            // //ViewBag.Products = products;
            // return View(products);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ProductDetails(int? id)
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
                                    


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(Product p)
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(int id)
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
        [Authorize(Roles = "Admin")]
        public ActionResult CreateProduct()
        {
            SelectList categ = new SelectList(categrepository.Items, "Id", "Name");
            ViewBag.Categ = categ;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateProduct(Product p)
        {
            ((DbSet<Product>)repository.Items).Add(p);
            await ((ProductRepository)repository).Context.SaveChangesAsync();

            //return View();
            return RedirectToAction("List");

        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProduct(int? id)
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditProduct(Product p)
        {
            ((ProductRepository)repository).Context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            await ((ProductRepository)repository).Context.SaveChangesAsync();
            return RedirectToAction("List");
        }






      //  [HttpPost]
        public ActionResult ProductAdminSearch(string name, int page = 1)
        {
            var players = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).Include(p => p.Category)
                .OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();
            if (players.Count <= 0)
            {
                //var plvm1 = new ProductListViewModel
                //{
                //    Products = new List<Product>() { new Product() },
                //    //Products = repository
                //    //           .Products
                //    //           .OrderBy(p => p.Id)
                //    //           .Skip((page - 1) * PageSize)
                //    //           .Take(PageSize).Include(p => p.Category),
                //    PagingInfo = new PagingInfo
                //    {
                //        //  TotalItems =repository.Items.Count(),
                //        TotalItems = 0,
                //        ItemsPerPage = PageSize,
                //        CurrentPage = page
                //    }
                //};
                //return PartialView(plvm1);
                return HttpNotFound("There are no such product");
              //  return PartialView();
            }
            var plvm = new ProductListViewModel
            {
                Products = players,
                //Products = repository
                //           .Products
                //           .OrderBy(p => p.Id)
                //           .Skip((page - 1) * PageSize)
                //           .Take(PageSize).Include(p => p.Category),
                PagingInfo = new PagingInfo
                {
                    //  TotalItems =repository.Items.Count(),
                    // TotalItems = players.Where(p => name == null || p.Name.Contains(name)).Count(),
                    TotalItems = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).Include(p => p.Category).ToList().Count(),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentItemSearch = name
            };

            return PartialView(plvm);

            //var products = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).ToList();
           // return PartialView(products);
        }
      // public ActionResult Search() { return View(); }


    }
}