using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities.UserAuthentication
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
