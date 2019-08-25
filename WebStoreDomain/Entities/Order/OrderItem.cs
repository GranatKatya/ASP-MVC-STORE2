using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebStoreDomain.Entities
{
   public  class OrderItem
    {
        [Required]
        [Key]
        public int Id { get; set; }//table new  users


        public int? UserInfoId { get; set; }
        public virtual UserInfo UserInfo { get; set; }

     //   public int? UserInfoId { get; set; } // notice nullabe foreign key!
       // public virtual UserInfo UserInfo { get; set; }

        //[Required]
        //[ForeignKey("UserInfo")]
        //public int UserInfoId { get; set; }




        public int? DeliveryMethodId { get; set; } // notice nullabe foreign key!
        public virtual DeliveryMethod DeliveryMethod { get; set; }

        //[Required]
        //[ForeignKey("DeliveryMethod")]
        //public int DeliveryMethodId { get; set; }




        public int? PaymentMethodId { get; set; } // notice nullabe foreign key!
        public virtual PaymentMethod PaymentMethod { get; set; }



        public virtual ICollection<Order> Orders { get; set; }



        //[Required]
        //[ForeignKey("PaymentMethod")]
        //public int PaymentMethodId { get; set; }



        //   public virtual  UserInfo UserInfo { get; set; }
        //  public virtual DeliveryMethod DeliveryMethod { get; set; } // table list 
        //   public virtual  PaymentMethod PaymentMethod { get; set; } // table list 





        //[InverseProperty("PaymentMethod")]
        //public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
        //[InverseProperty("DeliveryMethod")]
        //public virtual ICollection<DeliveryMethod> DeliveryMethods { get; set; }
        //[InverseProperty("UserInfo")]
        //public virtual ICollection<UserInfo> UserInfos { get; set; }



    }
}
