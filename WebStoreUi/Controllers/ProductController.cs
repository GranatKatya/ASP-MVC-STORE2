using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
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
        private int PageSize = 9;

        //public ProductController()
        //{
        //    repository = new ProductRepository();//2 зависимость
        //    categrepository = new CategoryRepository();
        //}

        public ProductController(IStoreRepository<Product> storeRepository, IStoreRepository<Category> storeRepository2)
        {
            repository = storeRepository; // there is no 2 injection 
            categrepository = storeRepository2;                          // categrepository = new CategoryRepository();
        }


        // GET: Product
        [AllowAnonymous]
        public ActionResult List(string searchparam  , string category, int? productname, int page = 1)
        {
            IQueryable<Product> arr = ((DbSet<Product>)repository.Items).Include("Category");

            //  if (!String.IsNullOrEmpty(productname) && !productname.Equals("Все"))
            if (productname != null && productname != 0)
            {
                page = 1;
                //int id = Int32.Parse(productname);
                arr = arr.Where(p => p.Id == productname);
            }
            List<Product> teams = ((DbSet<Product>)repository.Items).ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            teams.Insert(0, new Product { Name = "Все", Id = 0 });

            var plvm = new ProductListViewModel
            {


            Products = arr
                            .Where(p => category == null || p.Category.Name == category)
                                .Where(s => searchparam == null || s.Name.Contains(searchparam))
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

            //    Products = repository
            //                .Items
            //                .Where(p=>category == null || p.Category.Name == category)
            //                .OrderBy(p => p.Id)
            //                .Skip((page - 1) * PageSize)
            //                .Take(PageSize),
            
            PagingInfo = new PagingInfo
                {
                TotalItems = arr.Where(p => category == null || p.Category.Name == category)
                                .Where(s => searchparam == null || s.Name.Contains(searchparam)).Count(),
                //TotalItems = repository.Items.Where(p => category == null || p.Category.Name == category).Count(),
                // TotalItems = repository.Items.Count(p => category == null || p.Category.Name == "For body"),
                ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentCategory = category,
                CurrentItemSearch = searchparam,
                ProductNames = new SelectList(teams, "Id", "Name"),
            };

            return View(plvm);
        }
        [AllowAnonymous]
        public ActionResult PartislListForAjaxCategories(string category, int? productname, int page = 1)
        {
            IQueryable<Product> arr = ((DbSet<Product>)repository.Items).Include("Category");

            //  if (!String.IsNullOrEmpty(productname) && !productname.Equals("Все"))
            if (productname != null && productname != 0)
            {
                page = 1;
                //int id = Int32.Parse(productname);
                arr = arr.Where(p => p.Id == productname);
            }
            List<Product> teams = ((DbSet<Product>)repository.Items).ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            teams.Insert(0, new Product { Name = "Все", Id = 0 });

            var plvm = new ProductListViewModel
            {


                Products = arr
                            .Where(p => category == null || p.Category.Name == category)
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                //    Products = repository
                //                .Items
                //                .Where(p=>category == null || p.Category.Name == category)
                //                .OrderBy(p => p.Id)
                //                .Skip((page - 1) * PageSize)
                //                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    TotalItems = arr.Where(p => category == null || p.Category.Name == category).Count(),
                    //TotalItems = repository.Items.Where(p => category == null || p.Category.Name == category).Count(),
                    // TotalItems = repository.Items.Count(p => category == null || p.Category.Name == "For body"),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentCategory = category,
                ProductNames = new SelectList(teams, "Id", "Name"),
            };

            // return View(plvm);
            return PartialView(plvm);
        }




        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            //е удалось привести тип объекта "System.Data.Entity.Infrastructure.DbQuery`1[WebStoreDomain.Entities.Product]" 
            //к типу "System.Data.Entity.DbSet`1[WebStoreDomain.Entities.Product]".

           // Product p = repository.Items.Where(p3=>p3.Id == id).FirstOrDefault();
           

          //  DbQuery<Product> dbq = ((DbQuery<Product>) pr);
          //  DbSet<Product> dbs0 = (DbSet<Product>)dbq;
          // Product pp2 = dbs0.Find(id);
          //DbSet<Product> dbs = ((DbSet<Product>)pr);
          //Product pp =  dbs.Find(id);

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
        public async Task<ActionResult> Delete(Product p)
        {
            // Product p = db.Products.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
           // Product p1 = repository.Items.Where(pr => pr.Id == p.Id).FirstOrDefault();
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
        public async Task<ActionResult> Delete(int id)
        {
          //    Product p =  repository.Items.Where(pr => pr.Id == id).FirstOrDefault();
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
        public ActionResult Add()
        {
            SelectList categ = new SelectList(categrepository.Items, "Id", "Name");
            ViewBag.Categ = categ;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(Product p)
        {
            ((DbSet<Product>)repository.Items).Add(p);
            await ((ProductRepository)repository).Context.SaveChangesAsync();

            //return View();
            return RedirectToAction("List");

        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            // ModelState.IsValidField("Name"); 
            //  ModelState["Name"].Errors;
            // ModelState.AddModelError("Name", "ERROR");

           

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
        public async Task<ActionResult> Edit(Product p)
        {
            if (Regex.IsMatch(p.Name, @"^\w{3,}$"))
            {
                ModelState.AddModelError("Name","you entered wrong name");
            }


            if (ModelState.IsValid)
            {
                ((ProductRepository)repository).Context.Entry(p).State = System.Data.Entity.EntityState.Modified;
                await ((ProductRepository)repository).Context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(p);
        }



        [HttpPost]
        public ActionResult ProductsSearch(string name)
        {
            var products = ((DbSet<Product>)repository.Items).Where(p=>p.Name.Contains(name)).ToList();
            if (products.Count <= 0)
            {
                return HttpNotFound();
            }

            return PartialView(products);
        }
        public ActionResult Search() { return View(); }


        public ActionResult ProductSearch(string searchparam, string category, int? productname, int page = 1)
        {


            IQueryable<Product> arr = ((DbSet<Product>)repository.Items).Include("Category");

            //  if (!String.IsNullOrEmpty(productname) && !productname.Equals("Все"))
            if (productname != null && productname != 0)
            {
                page = 1;
                //int id = Int32.Parse(productname);
                arr = arr.Where(p => p.Id == productname);
            }
            List<Product> teams = ((DbSet<Product>)repository.Items).ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            teams.Insert(0, new Product { Name = "Все", Id = 0 });

            var plvm = new ProductListViewModel
            {


                Products = arr
                            .Where(p => category == null || p.Category.Name == category)
                            .Where(s=> searchparam == null || s.Name.Contains(searchparam))
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),

                //    Products = repository
                //                .Items
                //                .Where(p=>category == null || p.Category.Name == category)
                //                .OrderBy(p => p.Id)
                //                .Skip((page - 1) * PageSize)
                //                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    TotalItems = arr.Where(p => category == null || p.Category.Name == category)
                                        .Where(s => searchparam == null || s.Name.Contains(searchparam)).Count(),
                    //TotalItems = repository.Items.Where(p => category == null || p.Category.Name == category).Count(),
                    // TotalItems = repository.Items.Count(p => category == null || p.Category.Name == "For body"),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentCategory = category,
                CurrentItemSearch = searchparam,
                ProductNames = new SelectList(teams, "Id", "Name"),
            };




            //var players = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).Include(p => p.Category)
            //    .OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();
            //if (players.Count <= 0)
            //{
            //    //var plvm1 = new ProductListViewModel
            //    //{
            //    //    Products = new List<Product>() { new Product() },
            //    //    //Products = repository
            //    //    //           .Products
            //    //    //           .OrderBy(p => p.Id)
            //    //    //           .Skip((page - 1) * PageSize)
            //    //    //           .Take(PageSize).Include(p => p.Category),
            //    //    PagingInfo = new PagingInfo
            //    //    {
            //    //        //  TotalItems =repository.Items.Count(),
            //    //        TotalItems = 0,
            //    //        ItemsPerPage = PageSize,
            //    //        CurrentPage = page
            //    //    }
            //    //};
            //    //return PartialView(plvm1);
            //    return HttpNotFound("There are no such product");
            //    //  return PartialView();
            //}
            //var plvm = new ProductListViewModel
            //{
            //    Products = players,
            //    //Products = repository
            //    //           .Products
            //    //           .OrderBy(p => p.Id)
            //    //           .Skip((page - 1) * PageSize)
            //    //           .Take(PageSize).Include(p => p.Category),
            //    PagingInfo = new PagingInfo
            //    {
            //        //  TotalItems =repository.Items.Count(),
            //        // TotalItems = players.Where(p => name == null || p.Name.Contains(name)).Count(),
            //        TotalItems = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).Include(p => p.Category).ToList().Count(),
            //        ItemsPerPage = PageSize,
            //        CurrentPage = page
            //    },
            //    CurrentItemSearch = name
            //};

            return PartialView(plvm);

            //var products = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).ToList();
            // return PartialView(products);
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