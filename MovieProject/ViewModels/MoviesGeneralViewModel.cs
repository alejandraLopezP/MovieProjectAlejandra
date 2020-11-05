using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieProject.Models.DataBase;


namespace MovieProject.Models
{
    public class MoviesGeneralViewModel
    {
        public List<Movie> OldestMovie { get; set; }
        public List<Movie> NewestMovie { get; set; }
        public List<Movie> CheapestMovie { get; set; }
        public Order ExpensiveOrder { get; set; }
        public List<Movie> MostPopularMovie { get; set; }
    }
}