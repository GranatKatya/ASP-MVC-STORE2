using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Entities;
using WebStoreDomain.Entities.UserAuthentication;
using WebStoreUi.Models;
namespace WebStoreUi.Controllers
{
    public class AccountController : Controller
    {
        private StoreUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<StoreUserManager>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
                //return HttpContext.GetOwinContext().GetUserManager<IAuthenticationManager>();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { UserName = model.Email, Email = model.Email }; // password doesn t send here  / it s after registration
                IdentityResult result = await UserManager.CreateAsync(user, model.Password); // password 
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "User");
                    // return RedirectToAction("Login");
                    return RedirectToAction("List", "Product");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        foreach (string error in result.Errors)
                            ModelState.AddModelError("", error);

                        //    ModelState.AddModelError("", item);
                        // return View(model);
                    }

                }
            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Incorrect email or password");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("List", "Product");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}