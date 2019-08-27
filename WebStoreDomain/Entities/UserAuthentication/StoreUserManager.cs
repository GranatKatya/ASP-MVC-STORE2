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
    public class StoreUserManager : UserManager<User>
    {
        //class that manage  users 
        public StoreUserManager(IUserStore<User> userStore) : base(userStore)
        { }
        public static StoreUserManager Create(IdentityFactoryOptions<StoreUserManager> options, IOwinContext owinContext)
        {//new 
            StoreDbContext db = owinContext.Get<StoreDbContext>();
            StoreUserManager manger = new StoreUserManager(new UserStore<User>(db));
            //   return new StoreUserManager();
            return manger;
        }
    }
}
