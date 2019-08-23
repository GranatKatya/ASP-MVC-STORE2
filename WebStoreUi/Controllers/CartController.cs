using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Abstract;
using WebStoreDomain.Concrete;
using WebStoreDomain.Entities;
using WebStoreUi.Models;

namespace WebStoreUi.Controllers
{
    public class CartController : Controller
    {
        private IStoreRepository<Product> repository; //1 зависимость
       
      //  private int PageSize = 5;
        public CartController()
        {
            repository = new ProductRepository();//2 зависимость
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
            return Redirect(returnurl);
        }


       public  ActionResult Index(string returnurl)
        {
            CartIndexViewModel cartIndexViewModel = new CartIndexViewModel {Cart = this.Cart, ReturnUrl = returnurl };
            return View (cartIndexViewModel);
        }

        public ActionResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            return View(new ShippingDetails());
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
    }
}