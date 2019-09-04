using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStoreUi.Models
{
    public class Users_in_Role_ViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserRoles { get; set; }


        public string CurrentUser { get; set; }
    }
}