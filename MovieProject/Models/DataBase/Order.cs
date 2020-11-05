using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models.DataBase
{
    public class Order
    {
       [Key]
       public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }//para declarar la Foreign key

        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }

}