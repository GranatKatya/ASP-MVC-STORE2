using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;

namespace WebStoreDomain.Entities
{

    public class ShippingDetails
    {
        public ShippingDetails() { }
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Вставьте первый адрес доставки")]
        [Display(Name = "Первый адрес")]
        public string Line1 { get; set; }

        //[Display(Name = "Второй адрес")]
        //public string Line2 { get; set; }

        //[Display(Name = "Третий адрес")]
        //public string Line3 { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Input your correct email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Input your correct phone")]
        public string Phone { get; set; }




        //  [Required(ErrorMessage = "Choose  delivery method")]
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }


        // public int? PaymentId { get; set; }
        //   public string SelectedItemId { get; set; }
        //   public List<SelectListItem> Items { get; set; } 





        //   [Required(ErrorMessage = "Choose  payment method")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }



        public int SelectedOrderId { get; set; }
      //  public SelectList OrderTemplates { get; set; } //= new SelectList();



        public bool GiftWrap { get; set; }
    }

    //public class ShippingDetails
    // {
    //     [Required(ErrorMessage = "Укажите как вас зовут")]
    //     public string Name { get; set; }

    //     [Required(ErrorMessage = "Вставьте первый адрес доставки")]
    //     public string Line1 { get; set; }
    //     public string Line2 { get; set; }
    //     public string Line3 { get; set; }

    //     [Required(ErrorMessage = "Укажите город")]
    //     public string City { get; set; }

    //     [Required(ErrorMessage = "Укажите страну")]
    //     public string Country { get; set; }

    //     public bool GiftWrap { get; set; }
    // }
}
