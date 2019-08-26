using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Entities;

namespace WebStoreUi.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentState { get; set; }
    }

    public class OrderCartListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList DeliveryMrthods { get; set; }
        public SelectList PaymentMrthods { get; set; }
        public Order Order { get; set; }
        public Cart Cart { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
        public string ReturnUrl { get; set; }
     //   public bool IsNew { get; set; }
        public void Clear() {
            Order = new Order();
            Cart = new Cart();
            ShippingDetails = new ShippingDetails();
        }
    }
}