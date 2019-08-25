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

using WebStoreUi.Models;

namespace WebStoreUi.Controllers
{
    public class CartController : Controller
    {
        private IStoreRepository<Product> repository; //1 зависимость
        private IOrderProcessor orderProcessor;
        private IStoreRepository<UserInfo> userrepository;
        private IStoreRepository<OrderItem> orderItemRepository;
        private IStoreRepository<DeliveryMethod> deliverymethodsrepository;
        private IStoreRepository<PaymentMethod> paymentmethodsrepository;
        private IStoreRepository<Order> orderrepository;
        private IStoreRepository<CartItem> cartitempository;

    
        public CartController() { }

        //  private int PageSize = 5;
        public CartController(IStoreRepository<Product> repositoryp, IOrderProcessor processor, IStoreRepository<UserInfo> userrepositoryninject,
            IStoreRepository<OrderItem> orderItemRepositoryninject, IStoreRepository<DeliveryMethod> deliverymethodsrepositoryninlect,
            IStoreRepository<PaymentMethod> paymentmethodsrepositoryninject, IStoreRepository<Order> orderrepositoryninject, IStoreRepository<CartItem> cartitempositoryninject)
        {
            //  repository = new ProductRepository();//2 зависимость
            repository = repositoryp;
            orderProcessor = processor;
            userrepository = userrepositoryninject;
            orderItemRepository = orderItemRepositoryninject;
            deliverymethodsrepository = deliverymethodsrepositoryninlect;
            paymentmethodsrepository = paymentmethodsrepositoryninject;
            orderrepository = orderrepositoryninject;
            cartitempository = cartitempositoryninject;
        }
        // GET: Cart
        public ActionResult AddToCart(int productid, string returnurl)
        {
            Product product = repository.Items.FirstOrDefault(p=>p.Id == productid);
            if (product != null)
            {
                //add product to cart
                Cart.AddItem(product, 1);
            }
            // return Redirect(returnurl);
            return RedirectToAction("Index", new { returnurl= returnurl });
        }     
        
        public ActionResult RemoveFromCart(int productid, string returnurl) {
            Product product = repository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                Cart.RemoveItem(product);
            }
            //  return Redirect(returnurl);
            return RedirectToAction("Index", new { returnurl = returnurl });
        }

        public ActionResult RemoveFromCartDecrement(int productid, string returnurl)
        {
            Product product = repository.Items.FirstOrDefault(p => p.Id == productid);
            if (product != null)
            {
                Cart.RemoveItemDecremente(product);
            }
            //return Redirect(returnurl);
            return RedirectToAction("Index", new { returnurl = returnurl });
        }

        
       public  ActionResult Index(string returnurl)
        {
            CartIndexViewModel cartIndexViewModel = new CartIndexViewModel {Cart = this.Cart, ReturnUrl = returnurl };

            return View (cartIndexViewModel);
        }

        //public ActionResult Checkout(Cart cart, ShippingDetails shippingDetails)
        //{
        //    return View(new ShippingDetails());
        //}
        public ViewResult Checkout(Cart cart)
        {


            SelectList deliveryMethods = new SelectList(deliverymethodsrepository.Items, "idf", "Name");
            ViewBag.DeliveryMethods = deliveryMethods;
            SelectList paymentMethods = new SelectList(paymentmethodsrepository.Items, "PaymentMethodId", "Name");
            ViewBag.PaymentMethods = paymentMethods;

            ViewBag.Cart = this.Cart;
            //  ViewBag.ItemsRep = this.paymentmethodsrepository.Items;
            return View(new ShippingDetails());// { Items = new List<SelectListItem>() });//, OrderTemplates = new SelectList(paymentmethodsrepository.Items, "PaymentMethodId", "Name", 1) });
        }


        [HttpPost]
        async public Task<ViewResult>  Checkout( ShippingDetails shippingDetails)
        {
            //   ModelState.Clear();
          //  ModelStateDictionary.Add("s", shippingDetails);
          //  ModelState.Values.Remove(ModelState["cart"]);

         //   cart = Cart;
            if (Cart.Items.Count() == 0)
            {
                ModelState.AddModelError("error", "Извините, ваша корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                // if model is valid
                UserInfo userinfo = new UserInfo() { Id = 1, Name=shippingDetails.Name, SurName = shippingDetails.Surname, City = shippingDetails.City,
                                        Email = shippingDetails.Email, Phone = shippingDetails.Phone   };


                ((DbSet<UserInfo>)userrepository.Items).Add(userinfo);
              //  await((UserRepository)userrepository).Context.SaveChangesAsync();

                OrderItem orderItem = new OrderItem() {  UserInfo = userinfo, DeliveryMethodId = 3, PaymentMethodId = 3 };// , OrderItemId  = 9};
                ((DbSet<OrderItem>)orderItemRepository.Items).Add(orderItem);
              //  await ((OrderItemRepository)orderItemRepository).Context.SaveChangesAsync();


                Order order = new Order() { OrderItem = orderItem, State ="new" };                
                ((DbSet<Order>)orderrepository.Items).Add(order);
                // await ((OrdersRepository)orderrepository).Context.SaveChangesAsync();


             
                for (int i = 0; i < Cart.Items.Count; i++)
                {
                    CartItem cartItem = new CartItem() { ProductId = Cart.Items[i].Product.Id, Order = order, Quantity = Cart.Items[i].Quantity };
                  // Cart.Items[i].Order = order;
                    ((DbSet<CartItem>)cartitempository.Items).Add(cartItem);
                }

                await ((CartItemRepository)cartitempository).Context.SaveChangesAsync();

             





                orderProcessor.ProcessOrder(Cart, shippingDetails);
                Cart.Clear();
                return View("Completed");
            }
            else
            {

                //orderProcessor.ProcessOrder(cart, shippingDetails);
                //cart.Clear();
                //return View("Completed");

                return View(shippingDetails);
            }


        }





        private Cart Cart {
            get
            {
                Cart cart = (Cart)Session["Cart"];
                if(cart == null)
                {
                    cart = new Cart();
                    Session["Cart"] = cart;
                }
                return cart;
            }
        }

        public PartialViewResult Summary()
        {


            return PartialView(Cart);
        }
    }
}