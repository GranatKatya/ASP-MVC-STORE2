using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Abstract;
using WebStoreDomain.Entities;

namespace WebStoreDomain.Concrete.Order
{
    public class CartItemRepository : IStoreRepository<CartItem>
    {
        private StoreDbContext context = new StoreDbContext();
        public StoreDbContext Context { get { return context; } }

        //   public IEnumerable<Product> Products => context.Products;
        //   public IEnumerable<Category> Categories => context.Categories;

        public IEnumerable<CartItem> Items => context.CartItems;
    }
}
