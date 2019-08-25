using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebStoreDomain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength =2)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(2500, MinimumLength = 2)]
        public string Description { get; set; }
        [Range(5, 1000)]
        public int? Volume { get; set; }
        [Required]
        public bool InStoke { get; set; }
        public string Src { get; set; }

        //https://www.academia.edu/9584184/Getting_Started_with_Entity_Framework_6_Code_First_using_MVC_5

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        //public int? CartItemId { get; set; }
        //public CartItem CartItem { get; set; }



        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}

