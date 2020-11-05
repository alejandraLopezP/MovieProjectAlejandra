using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieProject.Models;
using MovieProject.Models.DataBase;
using Microsoft.AspNet.Identity;
using MovieProject.ViewModels;

namespace MovieProject.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
      

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var customer = db.Customers.FirstOrDefault(c => c.User_Id == currentUserId);

            if (customer == null)
            {
                return RedirectToAction("Create", "Customers", new { isProcessingCheckout = true });
            }

            List<MovyViewModel> movyList = new List<MovyViewModel>();
            var totalCart = 0.0m;

            foreach (string key in Session.Keys)
            {
                if (key.ToString() != "isCheckout")
                {
                    Movie movy = (from c in db.Movies
                                  where c.Title == key
                                  select c).ToList()[0];
                    int countMovy = int.Parse(Session[key].ToString());

                    totalCart += (movy.Price * countMovy);

                    movyList.Add(new MovyViewModel
                    {
                        Movy = movy,
                        Count = countMovy
                    });
                }
            }
            ViewBag.Total = totalCart;

            OrderCheckoutViewModel orderCheckout = new OrderCheckoutViewModel
            {
                Customer = customer,
                Movies = movyList
            };

            return View(orderCheckout);
        }

        public ActionResult CheckCustomer()
        {
            return View();
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingZip,BillingCity,DeliveryAddress,DeliveryZip,DeliveryCity,PhoneNo,EmailAddress")] Customer customer)
        {
            var currentUserId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == currentUserId);

            if (user != null)
            {
                customer.EmailAddress = user.Email;
                customer.User = user;
            }

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,PhoneNo,BillingAddress")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult CustomerOrder(string id)
        {
            var customer = db.Customers.Where(c => c.EmailAddress == id).FirstOrDefault();

            if(customer == null)
            {
                TempData["nonEmail"] = "This email doesn´t exist";
                return RedirectToAction("Orders");
            }

            var orders = (from order in db.Orders
                          where order.CustomerId == customer.Id
                          select order).ToList();

            var ordersWithDetails = orders.Select(o => new OrderDetailCustomerViewModel 
            {
                Order = o,
                OrderRow = db.OrderRows.Include(or => or.Movie).Where(or => or.OrderId == o.Id).ToList()
            }).ToList();

            ViewBag.Customer = customer;
            ViewBag.OrderWithDetails = ordersWithDetails;
            


            return View("CustomerOrder");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
