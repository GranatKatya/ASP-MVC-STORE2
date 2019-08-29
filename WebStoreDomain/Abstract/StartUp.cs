using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
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

          //  app.SetDefaultSignInAsAuthenticationType(WsFederationAuthenticationDefaults.AuthenticationType);


            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseGoogleAuthentication(new Microsoft.Owin.Security.Google.GoogleOAuth2AuthenticationOptions
            {
                ClientId = "773595035325-bifbn0p72q92d0tvfoa99smgadgsao85.apps.googleusercontent.com",
                ClientSecret = "gfstFx2NcGJQvRPaZKQIpOrM",
                AuthenticationType="Google", // name to find it 
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
               
              //  CallbackPath = new PathString("/Account/GoogleLoginCallback")
            });
        }
    }
}
