using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreDomain.Entities
{
   public class CartItem
    {
        [Required]
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Product")]
        // public string ProductId { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }


        public int Quantity { get; set; }
    }
}
