using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebStoreUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
              name: null,
              url: "",
              defaults: new { controller = "Product", action = "List", category= (string)null, page =1  }
              );

            //page
            //routes.MapRoute(
            //    name: "",
            //    url: "Page{page}",
            //    defaults: new { controller = "Product", action = "List" }
            //    );


            routes.MapRoute(
              name: "PagingRoute2",
              url: "{controller}/{action}/ProductPage{page}",
              defaults: new { controller = "Product", action = "List" }
              );

            routes.MapRoute(
               name: "PagingRoute4",
               url: "{controller}/{action}/CategoryPage{page}",
               defaults: new { controller = "Category", action = "List" }
               );


            //class


            routes.MapRoute(
                name: "",
                url: "{category}/Page{page}",
                defaults: new { controller = "Product", action = "List" }
                );

            //routes.MapRoute(
            //name: "",
            //url: "{action}/{category}/Page{page}"
            //);

            //routes.MapRoute(
            // name: "",
            // url: "{category}",
            // defaults: new { controller = "Product", action = "List", page = 1 }
            // );
            //classs


            routes.MapRoute(
                name: "PagingRoute1",
                url: "ProductPage{page}",
                defaults: new { controller = "Product", action = "List" }
                );
            routes.MapRoute(
              name: "PagingRoute3",
              url: "CategoryPage{page}",
              defaults: new { controller = "Category", action = "List" }
              );

          







            routes.MapRoute(
             name: "PagingRoute5",
             url: "Admin/Product{page}",
             defaults: new { controller = "AdminProduct", action = "List" }
             );

            routes.MapRoute(
           name: "PagingRoute6",
           url: "Admin/Category{page}",
           defaults: new { controller = "Category", action = "List" }
           );









            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );

           //config.MapHttpAttributeRoutes();

            //routes.MapRoute(
            //    name: "Default1",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Category", action = "List", id = UrlParameter.Optional }
            //);
        }
    }
}
