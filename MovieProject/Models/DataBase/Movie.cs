using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models.DataBase
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Director")]
        public string Director { get; set; }

        [Required]
        [Display(Name = "Release year")]
        public int ReleaseYear { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [MaxLength(50)]
        [Display(Name = "Genre")]
        public string Genre { get; set; }
    }
}