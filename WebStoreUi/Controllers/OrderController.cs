using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Abstract;
using WebStoreDomain.Concrete;
using WebStoreDomain.Concrete.Order;
using WebStoreDomain.Entities;
using System.Data.Entity;
using WebStoreUi.Models;

namespace WebStoreUi.Controllers
{
    public class OrderController : Controller
    {
        private IStoreRepository<Order> repository; //1 зависимость
        private int PageSize = 5;
        //  private int PageSize = 5;
        public OrderController(IStoreRepository<Order> repositoryninject)
        {
            //  repository = new ProductRepository();//2 зависимость
            repository = repositoryninject;
           
        }

        public ActionResult List(string state, int page = 1)
        {
            //   ((StoreDbContext)((OrdersRepository)repository).Context).ConContextOptions.LazyLoadingEnabled = false;
            //((DbSet<Order>)repository.Items).ContextOptions.LazyLoadingEnabled = false;


            IQueryable<Order> orders = ((DbSet<Order>)repository.Items)
                .Include(p => p.OrderItem) //.Select(x => x.UserInfo))
                                           // .ThenInclude(x => x.UserInfo)
                 .Include(p => p.OrderItem.UserInfo)
                   .Include(p => p.OrderItem.DeliveryMethod)
                     .Include(p => p.OrderItem.PaymentMethod);
            //  .OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize);


         


            var plvm = new OrderListViewModel
            {
                Orders = orders
                            .Where(p => state == null || p.State == state)
                            .OrderBy(p => p.Id)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                            //.Items
                            //.OrderBy(p => p.Id)
                            //.Skip((page - 1) * PageSize)
                            //.Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = orders.Where(p => state == null || p.State == state).Count(),
                  //  TotalItems = orders.Where(p => state == null || p.State == state).Count(),
                    //TotalItems = repository.Items.Where(p => category == null || p.Category.Name == category).Count(),
                    // TotalItems = repository.Items.Count(p => category == null || p.Category.Name == "For body"),
                    ItemsPerPage = PageSize,
                    CurrentPage = page
                },
                CurrentState = state
            };

            return View(plvm);
        }

        public async Task<ActionResult> OrderDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            IQueryable<Order> orders = ((DbSet<Order>)repository.Items)
                .Include(p => p.OrderItem)
                 .Include(p => p.OrderItem.UserInfo)
                   .Include(p => p.OrderItem.DeliveryMethod)
                     .Include(p => p.OrderItem.PaymentMethod)
                              .Include(p => p.CartItems)
                                .Include(p => p.CartItems);


            Order order = await orders.Where(p => p.Id == id).FirstOrDefaultAsync();
            // Находим в бд футболиста
           // Order o = await ((DbSet<Order>)repository.Items).FindAsync(id); 
                      
            if (order == null)
            {
                return HttpNotFound();
            }


            //SelectList cat = new SelectList(categrepository.Items, "Id", "Name", o.CategoryId);
            //ViewBag.Categ = cat;
            return View(order);

        }






        [HttpGet]
        public async Task<ActionResult> DeleteOrder(Order order)
        {
            if (order == null)
            {
                return HttpNotFound();
            }
            IQueryable<Order> orders = ((DbSet<Order>)repository.Items)
             .Include(p => p.OrderItem)
              .Include(p => p.OrderItem.UserInfo)
                .Include(p => p.OrderItem.DeliveryMethod)
                  .Include(p => p.OrderItem.PaymentMethod)
                           .Include(p => p.CartItems)
                             .Include(p => p.CartItems);

            Order o = await orders.Where(or => or.Id == order.Id).FirstOrDefaultAsync();
            if (o == null)
            {
                return HttpNotFound();
            }
            //SelectList cat = new SelectList(categrepository.Items, "Id", "Name", p.CategoryId);
            //ViewBag.Categ = cat;
            return View(o);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            IQueryable<Order> orders = ((DbSet<Order>)repository.Items)
           .Include(p => p.OrderItem)
            .Include(p => p.OrderItem.UserInfo)
              .Include(p => p.OrderItem.DeliveryMethod)
                .Include(p => p.OrderItem.PaymentMethod)
                         .Include(p => p.CartItems)
                           .Include(p => p.CartItems);

            Order o = await orders.Where(or => or.Id == id).FirstOrDefaultAsync();           
            if (o == null)
            {
                return HttpNotFound();
            }

            ((DbSet<Order>)repository.Items).Remove(o);     // ((DbContext)repository).SaveChanges();
            ((OrdersRepository)repository).Context.SaveChanges();
            return RedirectToAction("List");
        }




    }
}