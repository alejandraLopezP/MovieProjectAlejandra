using System;
using MovieProject.Models;
using MovieProject.Models.DataBase;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace MovieProject.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult GetToTalItemsInCart()
        {
            int totalMovieItems = 0;

            foreach (var sessionMovie in Session.Keys)
            {
                if(sessionMovie.ToString() != "isCheckout")
                {
                    var numberItems = (int)Session[sessionMovie.ToString()];
                    totalMovieItems += numberItems;
                }
            }

            return PartialView("_ShoppingCart", totalMovieItems);
           
        }

        public ActionResult CompleteOrder()
        {
            var currentUserId = User.Identity.GetUserId();

            var customer = (from c in db.Customers
                            where c.User_Id == currentUserId
                            select c).FirstOrDefault();

            if (customer == null)
            {
                return RedirectToAction("Create", "Customers", new { isProcessingCheckout = true });
            }
            else
            {

                List<Movie> movyList = new List<Movie>();
                var totalCart = 0.0M;

                foreach (string key in Session.Keys)
                {
                    if (key.ToString() != "isCheckout")
                    {
                        Movie movy = (from c in db.Movies
                                      where c.Title == key
                                      select c).FirstOrDefault();

                        movyList.Add(movy);

                        int countMovy = int.Parse(Session[key].ToString());

                        totalCart += (movy.Price * countMovy);
                    }

                }

                Order order = new Order();
                order.CustomerId = customer.Id;
                order.OrderDate = DateTime.Now;
                order.TotalPrice = totalCart;

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (Movie movy in movyList)
                {
                    //creamos el orderrows con la pelicual y el order insertado
                    OrderRow orderRow = new OrderRow();
                    orderRow.OrderId = order.Id;
                    orderRow.MovieId = movy.Id;
                    orderRow.Price = movy.Price;

                    db.OrderRows.Add(orderRow);
                }

                var rows = db.SaveChanges();

                Session.Clear();

                return RedirectToAction("FinishOrder");
            }

        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult FinishOrder()
        {
            ViewBag.messageFinish = "Thank you for your Order!";
            return View();
        }
    }
}