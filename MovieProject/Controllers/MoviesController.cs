using Microsoft.AspNet.Identity;
using MovieProject.Models;
using MovieProject.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MovieProject.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            var allMovies = db.Movies.ToList();
            return View(allMovies);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View("AddMovie");
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Director,ReleaseYear,ImageUrl,Genre,Price")] Movie movy)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movy);
                db.SaveChanges();

                TempData["MovieAdded"] = "Your movie has been added";

                return RedirectToAction("Index");
            }

            return View(movy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(int? Id, string page)
        {
            Movie movy = db.Movies.Find(Id);

            if (Session[movy.Title] != null)
            {
                Session[movy.Title] = (int)Session[movy.Title] + 1;
            }
            else
            {
                Session[movy.Title] = 1;
            }

            return RedirectToAction(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveToCart(int? MovieIdRemove)
        {
            Movie movy = db.Movies.Find(MovieIdRemove);

            if (Session[movy.Title] != null)
            {
                int count = (int)Session[movy.Title] - 1;
                //Session[movy.Title] = count > 0 ? count : null;
                if (count > 0)
                    Session[movy.Title] = count;
                else
                    Session.Remove(movy.Title);
            }

            return RedirectToAction("AllOrdersCart");
        }



        public ActionResult AllOrdersCart()
        {
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


            return View(movyList);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllOrdersCart(bool checkout)
        {
            Session["isCheckout"] = true;

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult MovieGeneral()
        {
            //OLDEST
            var oldestMovie = (from m in db.Movies
                               orderby m.ReleaseYear ascending
                               select m).Take(5).ToList();

            //NEWEST

            var newestMovie = (from m in db.Movies
                               orderby m.ReleaseYear
                               select m).Take(5).ToList();

            //CHEAPEST

            var cheapestMovie = (from m in db.Movies
                                 orderby m.Price
                                 select m).Take(5).ToList();
            //EXPENSIVE ORDER
            var expensiveOrder = (from o in db.Orders
                                  orderby o.TotalPrice descending
                                  select o).Include(o => o.Customer).FirstOrDefault();


            var mostPopularMovie = (from or in db.OrderRows
                                    group or by or.MovieId
                                    into groupMovie
                                    select new
                                    {
                                        movieId = groupMovie.Key,
                                        total = groupMovie.Count()
                                    }).OrderByDescending(m => m.total).Take(2).ToList();

            var listMovieIds = mostPopularMovie.Select(mp => mp.movieId);

            var movieMostPopular = db.Movies.Where(m =>
                        listMovieIds.Contains(m.Id)).ToList();
            //var movieMostPopular = db.Movies.Take(2, mostPopularMovie);

            MoviesGeneralViewModel obj = new MoviesGeneralViewModel();
            {
                obj.CheapestMovie = cheapestMovie;
                obj.NewestMovie = newestMovie;
                obj.OldestMovie = oldestMovie;
                obj.ExpensiveOrder = expensiveOrder;
                obj.MostPopularMovie = movieMostPopular;
               
            }

            return View("MoviesGeneral", obj);


        }


    }
}