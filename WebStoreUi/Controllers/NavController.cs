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
        private IStoreRepository<Order> orderrepository; //1 зависимость
        private List<string> ordersState = new List<string>() { "new", "payd", "send", "done" };
        public NavController(IStoreRepository<Category> repositorycat, IStoreRepository<Order> orderrepositoryninject)
        {
           // repository = new CategoryRepository();//2 зависимость
            repository = repositorycat;
            orderrepository = orderrepositoryninject;
        }
        // GET: Nav
        public ActionResult Menu()
        {
            return PartialView(repository.Items);
        }
        public ActionResult OrderFilter()
        {
            return PartialView(ordersState);
        }
    }
}