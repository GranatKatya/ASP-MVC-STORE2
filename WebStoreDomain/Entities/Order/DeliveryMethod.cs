using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities
{
    public class DeliveryMethod
    {
        [Key]
        public int DeliveryMethodId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        //public int OrderItemId { get; set; }
        //public virtual OrderItem OrderItem { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
    