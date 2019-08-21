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
    public class CategoryController : Controller
    {

        //dependency injection
        private IStoreRepository repository; //1 зависимость
        private int PageSize = 5;
        public CategoryController()
        {
            repository = new StoreRepository();//2 зависимость
        }

     //   public ActionResult Index()
        //{
        //    IEnumerable<Category> categories = db.Categories;

        //    ViewBag.Categories = categories;



        //    return View();
      //  }

        // GET: Category
        public ActionResult List(int page = 1)
        {
            var plvm = new CategoryListViewModel
            {
                Categories = repository
                            .Categories
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = repository.Categories.Count(),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                }
            };

            return View(plvm);
        }



        public ActionResult CategoryDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category team =   ((DbSet<Category>)repository.Categories).Include(t => t.Products).FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }




        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Category c)
        {

            ((DbSet<Category>)repository.Categories).Add(c);
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
            Category p = await ((DbSet<Category>)repository.Categories).FindAsync(id);
            if (p == null)
            {
                return HttpNotFound();
            }

            //SelectList cat = new SelectList(db.Categories, "Id", "Name", p.CategoryId);
            //ViewBag.Categ = cat;
            return View(p);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Category p)
        {
            ((StoreRepository)repository).Context.Entry(p).State = System.Data.Entity.EntityState.Modified;
            await ((StoreRepository)repository).Context.SaveChangesAsync();
            return RedirectToAction("List");
        }




        //[HttpGet]
        //public async Task<ActionResult> Delete(Category p)
        //{
        //    // Product p = db.Products.Find(id);
        //    if (p == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Category p1 = await ((DbSet<Category>)repository.Categories).FindAsync(p.Id);
        //    if (p1 == null)
        //    {
        //        return HttpNotFound();
        //    }


        //   ((StoreRepository)repository).Context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Products ADD CONSTRAINT Products_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories (Id) ON DELETE SET NULL");



        //    //SelectList cat = new SelectList(db.Categories, "Id", "Name", p.CategoryId);
        //    //ViewBag.Categ = cat;
        //    return View(p1);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Delete(int category)
        //{   
        //    Category p = await ((DbSet<Category>)repository.Categories).FindAsync(category);
        //    if (p == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ((DbSet<Category>)repository.Categories).Remove(p);
        //    // ((DbContext)repository).SaveChanges();
        //  await  ((StoreRepository)repository).Context.SaveChangesAsync();

        //    return RedirectToAction("List");
        //}







        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
           
          
            Category p1 = await ((DbSet<Category>)repository.Categories).FindAsync(id);
            if (p1 == null)
            {
                return HttpNotFound();
            }


          // ((StoreRepository)repository).Context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Products ADD CONSTRAINT Products_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories (Id) ON DELETE SET NULL");



            //SelectList cat = new SelectList(db.Categories, "Id", "Name", p.CategoryId);
            //ViewBag.Categ = cat;
            return View(p1);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Category category)
        {
            Category p = await ((DbSet<Category>)repository.Categories).FindAsync(category.Id);
            if (p == null)
            {
                return HttpNotFound();
            }

            ((DbSet<Category>)repository.Categories).Remove(p);
            // ((DbContext)repository).SaveChanges();
            await ((StoreRepository)repository).Context.SaveChangesAsync();

            return RedirectToAction("List");
        }



    }
}