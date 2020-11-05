using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models.DataBase
{
    public class OrderRow
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

    }


}