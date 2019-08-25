using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Abstract;
using WebStoreDomain.Entities;



namespace WebStoreDomain.Concrete.Order
{
    
  
    public class DeliveryMethodRepository : IStoreRepository<DeliveryMethod>
    {
        private StoreDbContext context = new StoreDbContext();
        public StoreDbContext Context { get { return context; } }

        //private StoreDbContext context = new StoreDbContext();
        //public StoreDbContext Context { get { return context; } }


        public  IEnumerable<DeliveryMethod> Items => context.DeliveryMethods;
    }
}
