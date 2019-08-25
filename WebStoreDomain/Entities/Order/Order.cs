using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities
{
   public class Order
    {
        [Required]
        [Key]
        public int Id { get; set; }//table new  users

        public int? OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }



        public virtual ICollection<CartItem> CartItems { get; set; }
        public string State { get; set; }
    }
}
