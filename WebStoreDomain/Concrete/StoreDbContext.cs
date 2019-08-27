using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Entities;
using WebStoreDomain.Entities.UserAuthentication;

namespace WebStoreDomain.Concrete
{
    public class StoreDbContext : IdentityDbContext<User>
    {
        public StoreDbContext() : base("StoreDbContext")
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //  public DbSet<Cart> Carts { get; set; }


        public DbSet<WebStoreDomain.Entities.Order> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }



        public static StoreDbContext Create()
        {//new 
            return new StoreDbContext();
        }

    }



    //public class StoreDbContext: DbContext
    // {
    //     public DbSet<Product> Products { get; set; }    
    //     public DbSet<Category> Categories { get; set; }


    //     public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    //     public DbSet<PaymentMethod> PaymentMethods { get; set; }

    //     public DbSet<UserInfo> Users { get; set; }
    //     public DbSet<OrderItem> OrderItems { get; set; }
    //     //  public DbSet<Cart> Carts { get; set; }


    //     public DbSet<WebStoreDomain.Entities.Order> Orders { get; set; }

    //     public DbSet<CartItem> CartItems { get; set; }
    // }


}
