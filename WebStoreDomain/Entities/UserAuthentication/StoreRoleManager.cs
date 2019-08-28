using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreDomain.Concrete;

namespace WebStoreDomain.Entities.UserAuthentication
{
    public class StoreRoleManager : RoleManager<Role>
    {
        public StoreRoleManager(IQueryableRoleStore<Role> store)
            : base(store) { }
            
        public static StoreRoleManager Create(
                IdentityFactoryOptions<StoreRoleManager> options,
                IOwinContext owinContext
            )
        {
            return new StoreRoleManager(
                new RoleStore<Role>(owinContext.Get<StoreDbContext>())
                );
        }
    }
}
