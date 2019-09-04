using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Abstract;
using WebStoreDomain.Entities;
using WebStoreDomain.Entities.UserAuthentication;
using WebStoreUi.Models;
using PagedList.Mvc;
using PagedList;

namespace WebStoreUi.Controllers
{
    public class UserController : Controller
    {

        //private IStoreRepository<Role> rolerepository; //1 зависимость


        //public UserController(IStoreRepository<Role> rolerepositoryninject)
        //{
        //    rolerepository = rolerepositoryninject;
        //}
        int pageSize = 5;

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
        public ActionResult Index(int? page)
        {
           // int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(UserManager.Users.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.MyRoles = RoleManager.Roles.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            ViewBag.MyRoles = RoleManager.Roles.ToList();
            if (ModelState.IsValid)
            {
                //   var role = RoleManager.Roles.SingleOrDefault(m => m.Id == (string)model.Roles);
                //  //  //    user.Roles.Add(new IdentityUserRole { RoleId = role.Id });
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                    UserRole = model.UserRole
                    //// //Roles = model.Roles
                    //  Roles.Add(new IdentityUserRole { RoleId = role.Id })
                };
                IdentityResult result = await UserManager.CreateAsync(user);
               // ViewBag.MyRoles = RoleManager.Roles.ToList();
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, model.UserRole);
                    return RedirectToAction("UsersWithRoles");
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
            ViewBag.Name = new SelectList(RoleManager.Roles
                                    .ToList(), "Name", "Name");
           



            //  IQueryable<Order> orders = UserManager.Include(p => p.OrderItem);
            User role = await UserManager.FindByIdAsync(id);
            if (role != null)
            {


                //ViewBag.country = new SelectList(RoleManager.Roles
                //                    .ToList(), "Id", "Name", role.UserRole);
                //ViewBag.OccupationList = new SelectList(RoleManager.Roles, "Id", "Name");
                Role myrole = new Role() { Name=" "};
                if ( !String.IsNullOrEmpty(role.UserRole))
                {
                     myrole = await RoleManager.FindByNameAsync(role.UserRole);
                }
              

                return View(new EditUserViewModel
                {
                    Id = role.Id,
                    Name = role.UserName,
                    Email = role.Email,
                    //  UserRole = ((List<IdentityUserRole>)role.Roles)[0].ToString() ,
                    //Roles = new SelectList(RoleManager.Roles
                    //                .ToList(), "Id", "Name", role.UserRole),
                    MyRoles = RoleManager.Roles.ToList(),
                    Myroletofind = myrole.Name
                }); 
            }
            return RedirectToAction("UsersWithRoles");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(EditUserViewModel model)
        {
          //  if (ModelState.IsValid)
            {
              
                User role = await UserManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Email = model.Email;
                    role.UserName = model.Name;
                    role.UserRole = model.UserRole;

                    IdentityResult result = await UserManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        var roles = await UserManager.GetRolesAsync(model.Id);
                        await this.UserManager.RemoveFromRolesAsync(model.Id, roles.ToArray());
                        await this.UserManager.AddToRoleAsync(role.Id, model.UserRole);
                        return RedirectToAction("UsersWithRoles");
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
            return RedirectToAction("UsersWithRoles");

        }


        public ActionResult UsersWithRoles(int? page)
        {
            var usersWithRoles = (from user in UserManager.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in RoleManager.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_ViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            int pageNumber = (page ?? 1);
            return View(usersWithRoles.ToPagedList(pageNumber, pageSize));
           // return View(usersWithRoles);
        }



        public ActionResult UsersWithRolesAjax(string param , int? page)
        {
            var usersWithRoles = (from user in UserManager.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in RoleManager.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_ViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames),
                                      CurrentUser = param
                                  });

            int pageNumber = (page ?? 1);
           // Where(u => u.Username.Contains(name)
            return PartialView(usersWithRoles.Where(p => param == null || p.Username.Contains(param) ).ToPagedList(pageNumber, pageSize));

            // return PartialView(usersWithRoles.Where(p => param == null || p.CurrentUser == param).ToPagedList(pageNumber, pageSize));
            // return View(usersWithRoles);
        }


        [HttpPost]
        public ActionResult UserAdminSearch(string name, int? page = 1)
        {
            //  var names = new string[] { name };
          //  var arr = UserManager.Users.Where(u =>u.UserName.Contains(name)).Include(p => p.Roles);
            var usersWithRoles = (from user in UserManager.Users   //where user.UserName == name  // name.Contains(user.UserName) //  
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in RoleManager.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_in_Role_ViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames),
                                      CurrentUser = name
                                  });
            var res = usersWithRoles.Where(u => u.Username.Contains(name));

            //var players = UserManager.Users.Where(p => p.Name.Contains(name)).Include(p => p.Category)
            //    .OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();
            if (res.ToList().Count <= 0)
            {
                return HttpNotFound("There are no such product");
                //  return PartialView();
            }


            int pageNumber = (page ?? 1);
            return PartialView(res.ToPagedList(pageNumber, pageSize));
            // return PartialView(plvm);

            //var products = ((DbSet<Product>)repository.Items).Where(p => p.Name.Contains(name)).ToList();
            // return PartialView(products);
        }

    }


}