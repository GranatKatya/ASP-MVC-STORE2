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
    public class AdminPanelController : Controller
    {   

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminPanel(string redirectUrl)
        {
            return RedirectToAction(redirectUrl);
            //  return View(model);
        }



    }
}