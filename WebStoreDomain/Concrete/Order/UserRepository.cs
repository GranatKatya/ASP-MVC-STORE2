using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Abstract;
using WebStoreDomain.Entities;

namespace WebStoreDomain.Concrete.Order
{
   //if i will need to work with users 
   // print table / add / remove / edit info about usesr
    public class UserRepository : IStoreRepository<UserInfo>
    {
        private StoreDbContext context = new StoreDbContext();
        public StoreDbContext Context { get { return context; } }

        //   public IEnumerable<Product> Products => context.Products;
        //   public IEnumerable<Category> Categories => context.Categories;

        public IEnumerable<UserInfo> Items => context.Users;
    }
}
