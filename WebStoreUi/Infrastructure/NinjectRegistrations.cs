using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using WebStoreDomain.Abstract;
using WebStoreDomain.Concrete;
using WebStoreDomain.Entities;

namespace WebStoreUi.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load() {
            Bind<IStoreRepository<Product>>().To<ProductRepository>();
            Bind<IStoreRepository<Category>>().To<CategoryRepository>();

        }
    }
}