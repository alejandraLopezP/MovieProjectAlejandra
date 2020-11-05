using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieProject.Models;
using MovieProject.Models.DataBase;

namespace MovieProject.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult doSearch(string search)
        {
            search = search.ToUpper();

            List<Movie> movies = (
                            from m in db.Movies
                            where m.Title.ToUpper().Contains(search) || m.Director.ToUpper().Contains(search) || m.Genre.ToUpper().Contains(search)
                            select m
                         ).ToList();

            //IF COINCIDENCES WHERE FOUND, OK, IF NOT, SHOW MESSAGE NOT MATCHING
            ViewBag.mensaje = movies != null && movies.Count > 0 ? "Data Found" : null;

            return View(movies);
        }
    }
}