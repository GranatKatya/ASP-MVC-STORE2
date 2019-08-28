using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Concrete;
using WebStoreDomain.Entities.UserAuthentication;

[assembly: OwinStartup(typeof(WebStoreDomain.Abstract.StartUp))]
namespace WebStoreDomain.Abstract
{
    public class StartUp
    {
        //configuration of OWIN engine
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<StoreDbContext>(StoreDbContext.Create);
            app.CreatePerOwinContext<StoreUserManager>(StoreUserManager.Create);
            app.CreatePerOwinContext<StoreRoleManager>(StoreRoleManager.Create);
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login")
            });
        }
    }
}
