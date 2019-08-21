using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Entities;


namespace WebStoreDomain.Abstract
{
   public interface IStoreRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Category> Categories { get; }
    }
}
