using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Abstract;
using WebStoreDomain.Concrete;
using WebStoreDomain.Entities;

namespace WebStoreUi.Controllers
{
    public class NavController : Controller
    {
        private IStoreRepository<Category> repository; //1 зависимость
        public NavController(IStoreRepository<Category> repositorycat)
        {
           // repository = new CategoryRepository();//2 зависимость
            repository = repositorycat;
        }
        // GET: Nav
        public ActionResult Menu()
        {
            return PartialView(repository.Items);
        }
    }
}