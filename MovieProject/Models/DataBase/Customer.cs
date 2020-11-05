using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieProject.Models.DataBase
{
    public class Customer
    {
        [Key]//para usar este tipo de anotaciones intalamos el Using arriba de Data Annotations
        public int Id { get; set; }//Primary key

        [StringLength(50)]
        [Required]//significa NOT NULL
        [Display(Name ="First Name")]//este es como un LABEL
        public string FirstName { get; set; }//sino colo la anotación lo toma omo un valor maximo:Nvarchar(Max) 

        [StringLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Billing Zip")]
        public string BillingZip { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Delivery Zip")]
        public string DeliveryZip { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Delivery City")]
        public string DeliveryCity { get; set; }


        [StringLength(255)]
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(50)]
        [Display(Name ="Phone number")]
        public string PhoneNo { get; set; }

        public string User_Id { get; set; }


        public virtual ICollection<Order> Orders { get; set; }//muchos tipos de ralaciones con la Orden

        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; set; }

    }
}