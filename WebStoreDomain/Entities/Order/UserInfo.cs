using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }//table new  users
        public string Name { get; set; }
        public string SurName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } //table new  users


        // public virtual ICollection<OrderItem> OrderItems { get; set; }  
        //    public OrderItem OrderItem { get; set; }


        //public int OrderItemId { get; set; }
        //public virtual OrderItem OrderItem { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }//LINQ    
    }
}
