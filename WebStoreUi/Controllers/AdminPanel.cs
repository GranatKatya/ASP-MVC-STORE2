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
    public class AdminPanel : Controller
    {   


        [HttpGet]
        public ActionResult AdminPanelView()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdminPanelView(RegisterViewModel model)
        {
         
            return View(model);
        }


    }
}