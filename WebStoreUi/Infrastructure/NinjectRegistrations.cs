using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using WebStoreDomain.Abstract;
using WebStoreDomain.Concrete;
using WebStoreDomain.Concrete.Order;
using WebStoreDomain.Entities;


namespace WebStoreUi.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load() {
            Bind<IStoreRepository<Product>>().To<ProductRepository>();
            Bind<IStoreRepository<Category>>().To<CategoryRepository>();

            Bind<IStoreRepository<UserInfo>>().To<UserRepository>();
            Bind<IStoreRepository<OrderItem>>().To<OrderItemRepository>();
            Bind<IStoreRepository<DeliveryMethod>>().To<DeliveryMethodRepository>();
            Bind<IStoreRepository<PaymentMethod>>().To<PaymentMethodRepository>();
            Bind<IStoreRepository<Order>>().To<OrdersRepository>();
            Bind<IStoreRepository<CartItem>>().To<CartItemRepository>();


       
// И с помощью вызова Bind<IRepository>().To<BookRepository>(); собственно устанавливается сопоставление между
//интерфейсом -зависимостью и конкретным классом этого интерфейса.




EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                  .AppSettings["Email.WriteAsFile"] ?? "false")
            };

          //  Bind<IOrderProcessor>().To<EmailOrderProcessor>();
            Bind<IOrderProcessor>().To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);
        }
    }
}