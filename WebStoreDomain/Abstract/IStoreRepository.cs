using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Entities;


namespace WebStoreDomain.Abstract
{
   public interface IStoreRepository<T>
    {
        IEnumerable<T> Items { get; }
       // IEnumerable<Category> Categories { get; }
       //add
       //delete
       //edite
    }
}
