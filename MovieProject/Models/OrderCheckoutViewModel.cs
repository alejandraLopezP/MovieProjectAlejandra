using MovieProject.Models.DataBase;
using System.Collections.Generic;

namespace MovieProject.Models
{
    public class OrderCheckoutViewModel
    {
        public Customer Customer { get; set; }
        public List<MovyViewModel> Movies { get; set; }
    }
}