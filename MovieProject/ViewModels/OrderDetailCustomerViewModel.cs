using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieProject.Models.DataBase;
using MovieProject.ViewModels;
using MovieProject.Controllers;

namespace MovieProject.ViewModels
{
    public class OrderDetailCustomerViewModel
    {
        public Order Order { get; set; }
        public List<OrderRow> OrderRow { get; set; }
        public Customer Customer { get; set; }
    }
}