using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreDomain.Entities.UserAuthentication;

namespace WebStoreUi.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
 
        [Required]
        [Display(Name="UserRole")]
        public string UserRole { get; set; }
    }
}