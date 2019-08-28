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
        private IStoreRepository<Product> productrepository; //1 зависимость
        private IStoreRepository<DeliveryMethod> deliverymethodrepository; //1 зависимость
        private IStoreRepository<PaymentMethod> paymentmethodrepository; //1 зависимость
       
        private IStoreRepository<UserInfo> userrepository;
        private IStoreRepository<OrderItem> orderItemRepository;     
        private IStoreRepository<CartItem> cartitempository;


        private int PageSize = 5;
        //  private int PageSize = 5;
        public OrderController(IStoreRepository<Order> repositoryninject, IStoreRepository<Product> productrepositoryninjecct,
                                    IStoreRepository<DeliveryMethod> deliverymethodrepositoryninject , IStoreRepository<PaymentMethod> paymentmethodrepositoryninject,
                                    IStoreRepository<UserInfo> userrepositoryninject, IStoreRepository<OrderItem> orderItemRepositoryninject, IStoreRepository<CartItem> cartitempositoryninject)
        {
            //  repository = new ProductRepository();//2 зависимость
            repository = repositoryninject;
            productrepository = productrepositoryninjecct;
            deliverymethodrepository = deliverymethodrepositoryninject;
            paymentmethodrepository = paymentmethodrepositoryninject;
            userrepository = userrepositoryninject;
            orderItemRepository = orderItemRepositoryninject;
            cartitempository = cartitempositoryninject;

        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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



      //  OrderCartListViewModel OrderCartListViewModel;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateOrder(string returnUrl)
        {          
        //    List<Product> teams = ((DbSet<Product>)productrepository.Items).ToList();
        //    List<DeliveryMethod> deliverymethods = ((DbSet<DeliveryMethod>)deliverymethodrepository.Items).ToList();
        //    List<PaymentMethod> paymentmethods = ((DbSet<PaymentMethod>)paymentmethodrepository.Items).ToList();

           
            //OrderCartListViewModel = new OrderCartListViewModel()
            //{
            //    Order = new Order(),
            //    Cart = new Cart(),
            //    Products = teams,
            //    ShippingDetails = new ShippingDetails(),
            //    DeliveryMrthods = new SelectList(deliverymethods, "DeliveryMethodId", "Name"),
            //    PaymentMrthods = new SelectList(paymentmethods, "PaymentMethodId", "Name")
            //};

            return View(OrderCartListViewModel);
          
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateOrder(OrderCartListViewModel orderCartListViewModel)
        {
            //if (OrderCartListViewModel.Cart.Items.Count() == 0)
            //{
            //    ModelState.AddModelError("error", "Извините, ваша корзина пуста!");
            //}

            //if (ModelState.IsValid)
            //{
                OrderCartListViewModel.ShippingDetails = orderCartListViewModel.ShippingDetails;
                //// if model is valid
                //UserInfo userinfo = new UserInfo()
                //{
                //    Id = 1,
                //    Name = shippingDetails.Name,
                //    SurName = shippingDetails.Surname,
                //    City = shippingDetails.City,
                //    Email = shippingDetails.Email,
                //    Phone = shippingDetails.Phone
                //};


                //((DbSet<UserInfo>)userrepository.Items).Add(userinfo);
                ////  await((UserRepository)userrepository).Context.SaveChangesAsync();

                //OrderItem orderItem = new OrderItem() { UserInfo = userinfo, DeliveryMethodId = 3, PaymentMethodId = 3 };// , OrderItemId  = 9};
                //((DbSet<OrderItem>)orderItemRepository.Items).Add(orderItem);
                ////  await ((OrderItemRepository)orderItemRepository).Context.SaveChangesAsync();


                //Order order = new Order() { OrderItem = orderItem, State = "new" };
                //((DbSet<Order>)orderrepository.Items).Add(order);
                //// await ((OrdersRepository)orderrepository).Context.SaveChangesAsync();



                //for (int i = 0; i < Cart.Items.Count; i++)
                //{
                //    CartItem cartItem = new CartItem() { ProductId = Cart.Items[i].Product.Id, Order = order, Quantity = Cart.Items[i].Quantity };
                //    // Cart.Items[i].Order = order;
                //    ((DbSet<CartItem>)cartitempository.Items).Add(cartItem);
                //}

                //await ((CartItemRepository)cartitempository).Context.SaveChangesAsync();







                //orderProcessor.ProcessOrder(Cart, shippingDetails);
                //Cart.Clear();
             //   return View("Completed"); 
                 return RedirectToAction("CreateOrderCart");
            //}
            //else
            //{ 
            //    return View(orderCartListViewModel);
            //}
        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateOrderAdmin()
        {
            if (Cart.Items.Count() == 0)
            {
                ModelState.AddModelError("error", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                // if model is valid
                UserInfo userinfo = new UserInfo()
                {
                    //Id = 1,
                    Name = OrderCartListViewModel.ShippingDetails.Name,
                    SurName = OrderCartListViewModel.ShippingDetails.Surname,
                    City = OrderCartListViewModel.ShippingDetails.City,
                    Email = OrderCartListViewModel.ShippingDetails.Email,
                    Phone = OrderCartListViewModel.ShippingDetails.Phone
                };


                ((DbSet<UserInfo>)userrepository.Items).Add(userinfo);
                //  await((UserRepository)userrepository).Context.SaveChangesAsync();

                OrderItem orderItem = new OrderItem() { UserInfo = userinfo, DeliveryMethodId = OrderCartListViewModel.ShippingDetails.DeliveryMethodId, PaymentMethodId = OrderCartListViewModel.ShippingDetails.PaymentMethodId };// , OrderItemId  = 9};
                ((DbSet<OrderItem>)orderItemRepository.Items).Add(orderItem);
                //  await ((OrderItemRepository)orderItemRepository).Context.SaveChangesAsync();


                Order order = new Order() { OrderItem = orderItem, State = "new" };
                ((DbSet<Order>)repository.Items).Add(order);
                // await ((OrdersRepository)orderrepository).Context.SaveChangesAsync();



                for (int i = 0; i < Cart.Items.Count; i++)
                {
                    CartItem cartItem = new CartItem() { ProductId = Cart.Items[i].Product.Id, Order = order, Quantity = Cart.Items[i].Quantity };
                    // Cart.Items[i].Order = order;
                    ((DbSet<CartItem>)cartitempository.Items).Add(cartItem);
                }

                await ((CartItemRepository)cartitempository).Context.SaveChangesAsync();



               // orderProcessor.ProcessOrder(Cart, shippingDetails);
                Cart.Clear();
                OrderCartListViewModel.Clear();
                return View("Completed");
            }
            else
            {

                //orderProcessor.ProcessOrder(cart, shippingDetails);
                //cart.Clear();
                //return View("Completed");

                return View(OrderCartListViewModel);
            }


        }


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditOrderAdmin()
        {
            if (Cart.Items.Count() == 0)
            {
                ModelState.AddModelError("error", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {


         //       IQueryable<Order> orders = ((DbSet<Order>)repository.Items)
         //.Include(p => p.OrderItem)
         // .Include(p => p.OrderItem.UserInfo)
         //   .Include(p => p.OrderItem.DeliveryMethod)
         //     .Include(p => p.OrderItem.PaymentMethod)
         //              .Include(p => p.CartItems)
         //                .Include(p => p.CartItems);
         //       Order order = await orders.Where(p => p.Id == OrderCartListViewModel.Order.Id).FirstOrDefaultAsync();
         //       ((OrdersRepository)repository).Context.Entry(order).State = System.Data.Entity.EntityState.Modified;
         //       await ((OrdersRepository)repository).Context.SaveChangesAsync();




                //// if model is valid
                UserInfo userinfo = new UserInfo()
                {
                    Id = OrderCartListViewModel.Order.OrderItem.UserInfo.Id,
                    Name = OrderCartListViewModel.Order.OrderItem.UserInfo.Name,
                    SurName = OrderCartListViewModel.Order.OrderItem.UserInfo.SurName,
                    City = OrderCartListViewModel.Order.OrderItem.UserInfo.City,
                    Email = OrderCartListViewModel.Order.OrderItem.UserInfo.Email,
                    Phone = OrderCartListViewModel.Order.OrderItem.UserInfo.Phone
                };
                ((UserRepository)userrepository).Context.Entry(userinfo).State = System.Data.Entity.EntityState.Modified;
                await ((UserRepository)userrepository).Context.SaveChangesAsync();


                //((DbSet<UserInfo>)userrepository.Items).Add(userinfo);
                ////  await((UserRepository)userrepository).Context.SaveChangesAsync();

                // OrderItem orderItem = new OrderItem() {Id = OrderCartListViewModel.Order.OrderItem.Id, UserInfoId = userinfo.Id, UserInfo = userinfo, DeliveryMethodId = OrderCartListViewModel.Order.OrderItem.DeliveryMethodId, PaymentMethodId = OrderCartListViewModel.Order.OrderItem.PaymentMethodId };// , OrderItemId  = 9};
                OrderItem orderItem = new OrderItem() { Id = OrderCartListViewModel.Order.OrderItem.Id, UserInfoId = userinfo.Id, DeliveryMethodId = OrderCartListViewModel.Order.OrderItem.DeliveryMethodId, PaymentMethodId = OrderCartListViewModel.Order.OrderItem.PaymentMethodId };// , OrderItemId  = 9};
                ((OrderItemRepository)orderItemRepository).Context.Entry(orderItem).State = System.Data.Entity.EntityState.Modified;
                await ((OrderItemRepository)orderItemRepository).Context.SaveChangesAsync();                                                                                                                                                                                                                                                                                            ////     ((DbSet<OrderItem>)orderItemRepository.Items).Add(orderItem);
                                                                                                                                                                                                                                                                                                                                                                                        ////  await ((OrderItemRepository)orderItemRepository).Context.SaveChangesAsync();


                //Order order = new Order() {Id = OrderCartListViewModel.Order.Id, OrderItemId = orderItem.Id,  OrderItem = orderItem, State = "change" };
                Order order = new Order() { Id = OrderCartListViewModel.Order.Id, OrderItemId = orderItem.Id, OrderItem = orderItem, State = "change" };

                //   order.CartItems = OrderCartListViewModel.Cart.Items;
                order.CartItems = new List<CartItem>();
                //   for (int i = 0; i < OrderCartListViewModel.Cart.Items.Count; i++)
                var res = ((DbSet<CartItem>)cartitempository.Items).Where(or=> or.OrderId == OrderCartListViewModel.Order.Id).ToList();
                  for (int i = 0; i < res.Count; i++)
                  {
                    //CartItem cartItem = new CartItem()
                    //{
                    //    Quantity = OrderCartListViewModel.Cart.Items[i].Quantity,
                    //    Id = OrderCartListViewModel.Cart.Items[i].Id,
                    //    OrderId = order.Id,
                    //    ProductId = OrderCartListViewModel.Cart.Items[i].ProductId
                    //};

                  //  order.CartItems.Add(cartItem);
                    /////////////////////////////////////////
                    //CartItem o =  ((DbSet<CartItem>)cartitempository.Items).Where(or => or.Id == OrderCartListViewModel[i].Id).FirstOrDefault();
                    //if (o != null)
                    //{

                    ((DbSet<CartItem>)cartitempository.Items).Remove(res[i]);
                   

                    // ((DbSet<CartItem>)cartitempository.Items).Remove(o);     // ((DbContext)repository).SaveChanges();

                }
                for (int i = 0; i < OrderCartListViewModel.Cart.Items.Count; i++)
                {
                    CartItem cartItem = new CartItem { Quantity = OrderCartListViewModel.Cart.Items[i].Quantity, OrderId = OrderCartListViewModel.Order.Id, ProductId = OrderCartListViewModel.Cart.Items[i].Product.Id };
                    ((DbSet<CartItem>)cartitempository.Items).Add(cartItem);
                }

                ((CartItemRepository)cartitempository).Context.SaveChanges();


                //////////((DbSet<Order>)repository.Items).Remove(o);     // ((DbContext)repository).SaveChanges();
                //////////((OrdersRepository)repository).Context.SaveChanges();


                //   ((OrdersRepository)repository).Context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                ((OrdersRepository)repository).Context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                //((OrdersRepository)repository).Context.As.Attach(order);
                await ((OrdersRepository)repository).Context.SaveChangesAsync();
                //  ((OrdersRepository)repository).Context.Entry(order).State = System.Data.Entity.EntityState.Detached;

                //  ((DbSet<Order>)repository.Items).Add(order);
                //// await ((OrdersRepository)orderrepository).Context.SaveChangesAsync();



                //for (int i = 0; i < Cart.Items.Count; i++)
                //{
                //    CartItem cartItem = new CartItem() { ProductId = Cart.Items[i].Product.Id, Order = order, Quantity = Cart.Items[i].Quantity };
                //    // Cart.Items[i].Order = order;
                //    ((DbSet<CartItem>)cartitempository.Items).Add(cartItem);
                //}

                //await ((CartItemRepository)cartitempository).Context.SaveChangesAsync();



                // orderProcessor.ProcessOrder(Cart, shippingDetails);
                Cart.Clear();
                OrderCartListViewModel.Clear();
                return View("Completed");
            }
            else
            {

                //orderProcessor.ProcessOrder(cart, shippingDetails);
                //cart.Clear();
                //return View("Completed");

                return View(OrderCartListViewModel);
            }


        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditOrder(int? id)
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
            if (order == null)
            {
                return HttpNotFound();
            }
            OrderCartListViewModel.Order = order;
           
            foreach (CartItem item in OrderCartListViewModel.Order.CartItems)
            {
                Cart.Items.Add(item);
            }
         
            OrderCartListViewModel.Cart = Cart;
            return View(OrderCartListViewModel);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditOrder(OrderCartListViewModel orderCartListViewModel)
        {
            OrderCartListViewModel.ShippingDetails = orderCartListViewModel.ShippingDetails;
            int iduser = OrderCartListViewModel.Order.OrderItem.UserInfo.Id;
            OrderCartListViewModel.Order.OrderItem.UserInfo = orderCartListViewModel.Order.OrderItem.UserInfo;
            OrderCartListViewModel.Order.OrderItem.UserInfo.Id = iduser;
            OrderCartListViewModel.Order.OrderItem.DeliveryMethod = orderCartListViewModel.Order.OrderItem.DeliveryMethod;
            OrderCartListViewModel.Order.OrderItem.PaymentMethod = orderCartListViewModel.Order.OrderItem.PaymentMethod;
            OrderCartListViewModel.Order.OrderItem.DeliveryMethodId = orderCartListViewModel.Order.OrderItem.DeliveryMethodId;
            OrderCartListViewModel.Order.OrderItem.PaymentMethodId = orderCartListViewModel.Order.OrderItem.PaymentMethodId;
            //  OrderCartListViewModel.Cart = orderCartListViewModel.Cart;

            return RedirectToAction("EditOrderCart");           
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditOrderCart()
        {
            return View(OrderCartListViewModel);
        }










        [Authorize(Roles = "Admin")]
        public ActionResult CreateOrderCart()
        {
            return View(OrderCartListViewModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddToCart(int productid, OrderCartListViewModel orderCartListViewModel, string returnurl )
        {
            Product product = productrepository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                //add product to cart
                Cart.AddItem(product, 1);
                OrderCartListViewModel.Cart = Cart;
               // OrderCartListViewModel.Cart = Cart;
              //  OrderCartListViewModel.Order.CartItems.Add(Cart.Items[Cart.Items.Count-1]);

            }
            // return Redirect(returnurl);
            // return RedirectToAction("CreateOrder", new { returnurl = returnurl});

          
            return RedirectToAction("CreateOrderCart");


        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddToCartEditMethod (int productid, OrderCartListViewModel orderCartListViewModel, string returnurl )
        {
            Product product = productrepository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                //add product to cart
                Cart.AddItem(product, 1);
                OrderCartListViewModel.Cart = Cart;
                // OrderCartListViewModel.Cart = Cart;
                //  OrderCartListViewModel.Order.CartItems.Add(Cart.Items[Cart.Items.Count-1]);

            }
            // return Redirect(returnurl);
            // return RedirectToAction("CreateOrder", new { returnurl = returnurl});


            return RedirectToAction("EditOrderCart");


        }


        [Authorize(Roles = "Admin")]
        public ActionResult RemoveFromCart(int productid, string returnurl )
        {
            Product product = productrepository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                Cart.RemoveItem(product);
               
            }
            //  return Redirect(returnurl);
            // return RedirectToAction("List", new { returnurl = returnurl });
           
            //if (returnurl != null)
            //{
            //    return RedirectToAction("EditOrderCart");
            //}
            return RedirectToAction("CreateOrderCart");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveFromCartEditMethod(int productid, string returnurl)
        {
            Product product = productrepository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                Cart.RemoveItem(product);
                OrderCartListViewModel.Cart = Cart;
            }
            return RedirectToAction("EditOrderCart");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveFromCartDecrement(int productid, string returnurl= null)
        {
            Product product = productrepository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                Cart.RemoveItemDecremente(product);
            }
            //return Redirect(returnurl);
           // return RedirectToAction("List", new { returnurl = returnurl });
          
            //if (returnurl != null)
            //{
            //    return RedirectToAction("EditOrderCart");
            //}
            return RedirectToAction("CreateOrderCart");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveFromCartDecrementEditMethod(int productid, string returnurl = null)
        {
            Product product = productrepository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                Cart.RemoveItemDecremente(product);
                OrderCartListViewModel.Cart = Cart;
            }
           
            return RedirectToAction("EditOrderCart");
        }


        private Cart Cart
        {
            get
            {
                Cart cart = (Cart)Session["OrderCart"];
                if (cart == null)
                {
                    cart = new Cart();
                    Session["OrderCart"] = cart;
                }
                return cart;
            }
        }

        private OrderCartListViewModel OrderCartListViewModel
        {
            get
            {
                OrderCartListViewModel orderCartListViewModel = (OrderCartListViewModel)Session["OrderCartListViewModel"];
                if (orderCartListViewModel == null)
                {

                List<Product> teams = ((DbSet<Product>)productrepository.Items).ToList();
                List<DeliveryMethod> deliverymethods = ((DbSet<DeliveryMethod>)deliverymethodrepository.Items).ToList();
                List<PaymentMethod> paymentmethods = ((DbSet<PaymentMethod>)paymentmethodrepository.Items).ToList();

                orderCartListViewModel = new OrderCartListViewModel()
                    {
                        Order = new Order(),
                        Cart = new Cart(),
                        Products = teams,
                        ShippingDetails = new ShippingDetails(),
                        DeliveryMrthods = new SelectList(deliverymethods, "DeliveryMethodId", "Name"),
                        PaymentMrthods = new SelectList(paymentmethods, "PaymentMethodId", "Name")
                    }; 
                    Session["OrderCartListViewModel"] = orderCartListViewModel;
                }
                return orderCartListViewModel;
            }
        }
               
    }
}