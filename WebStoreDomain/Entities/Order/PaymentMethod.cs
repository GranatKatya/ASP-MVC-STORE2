using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities
{
    public class PaymentMethod
    {
        public PaymentMethod() { }
        [Key]
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }

        //public int OrderItemId { get; set; }
        //public virtual OrderItem OrderItem { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
