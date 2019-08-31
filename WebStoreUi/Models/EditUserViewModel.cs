using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [Required]
        [Display(Name = "UserRole")]
        public string UserRole { get; set; }

        public List<Role> MyRoles{ get; set; }
        public string  Myroletofind { get; set; }





        public int? UserType { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        //public int DeliveryMethodId { get; set; }
        //public DeliveryMethod DeliveryMethod { get; set; }

        public IEnumerable<SelectListItem> OccupationId { get; set; }
        //  public int OccupationId { get; set; }
        public virtual IEnumerable<SelectListItem> Occupation { get; set; } 

    }
}