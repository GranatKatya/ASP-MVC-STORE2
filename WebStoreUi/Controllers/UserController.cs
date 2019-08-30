using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Abstract;
using WebStoreDomain.Entities.UserAuthentication;
using WebStoreUi.Models;

namespace WebStoreUi.Controllers
{
    public class UserController : Controller
    {

        //private IStoreRepository<Role> rolerepository; //1 зависимость


        //public UserController(IStoreRepository<Role> rolerepositoryninject)
        //{
        //    rolerepository = rolerepositoryninject;
        //}


        private StoreUserManager UserManager
        {
            get
            {
                return HttpContext
                    .GetOwinContext()
                    .GetUserManager<StoreUserManager>();
            }
        }

        private StoreRoleManager RoleManager
        {
            get
            {
                return HttpContext
                    .GetOwinContext()
                    .GetUserManager<StoreRoleManager>();
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            CreateUserViewModel CreateUserViewModel = new CreateUserViewModel();
          
            if (CreateUserViewModel == null)
            {

                List<Role> teams = RoleManager.Roles.ToList();
                CreateUserViewModel = new CreateUserViewModel()
                {
                    RolesList = teams
                };
                //var Users = db.Users.Include(u => u.Roles);
            }
            return View(CreateUserViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
             //   var role = RoleManager.Roles.SingleOrDefault(m => m.Id == (string)model.Roles);
        //  //  //    user.Roles.Add(new IdentityUserRole { RoleId = role.Id });
                IdentityResult result = await UserManager.CreateAsync(new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                   //// //Roles = model.Roles
                  //  Roles.Add(new IdentityUserRole { RoleId = role.Id })
            });

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }


        public async Task<ActionResult> Edit(string id)
        {
            User role = await UserManager.FindByIdAsync(id);
            if (role != null)
            {
                return View(new EditUserViewModel { Id = role.Id, Name = role.UserName, Email = role.Email });
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User role = await UserManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Email = model.Email;
                    role.UserName = model.Name;
                    IdentityResult result = await UserManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Что-то пошло не так");
                    }
                }
            }
            return View(model);
        }


        public async Task<ActionResult> Delete(string id)
        {
            User role = await UserManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");

        }

    }


}