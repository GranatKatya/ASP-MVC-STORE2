using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities
{
   public class Cart
    {
        private List<CartItem> items;
        public List<CartItem> Items => items; //c# 6 get 
        public Cart()
        {
            items = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            CartItem item = items.Where(i => i.Product.Id == product.Id).FirstOrDefault();// First exequte query
            if (item == null)
            {
                items.Add(new CartItem {Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItemDecremente(Product product)
        {
            CartItem item = items.Where(i => i.Product.Id == product.Id).FirstOrDefault();// First exequte query
            if (item != null)
            {
                if (item.Quantity ==1)
                {
                    items.RemoveAll(i => i.Product.Id == product.Id);
                }
                else if (item.Quantity >= 1)
                {

                    item.Quantity -= 1;
                }              
            }
        }

        public void RemoveItem(Product product)
        {
            items.RemoveAll(i=>i.Product.Id == product.Id);
        }
        public void Clear() {
            items.Clear();
        }
        public decimal ComputeTotalValue() {
            return items.Sum(i=>i.Product.Price * i.Quantity);
        }
    }
}
