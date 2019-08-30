using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Entities.UserAuthentication;

namespace WebStoreUi.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public SelectList Roles { get; set; }
        public IEnumerable<Role> Products { get; set; }
    }
}