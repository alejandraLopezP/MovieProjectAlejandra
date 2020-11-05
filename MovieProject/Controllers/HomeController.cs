using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieProject.Models;
using Microsoft.AspNet.Identity;
using MovieProject.Models.DataBase;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var allMovies = db.Movies.ToList();

            return View("Index", allMovies);//no me deja mostrar el Index de MOVIE:el cual se encuentra en SHARED
        }

        //ApplicationDbContext con = new ApplicationDbContext();
        //public ActionResult Index()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        if (IsAdmin())

        //        {

        //            ViewBag.Flg = 1;
        //        }
        //        else
        //        {
        //            ViewBag.Flg = 0;
        //        }

        //        return RedirectToAction("CreateRole", "Account");
        //    }
        //    return View();
        //}

        //public Boolean IsAdmin()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
        //        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(con));
        //        var s = UserManager.GetRoles(user.GetUserId());
        //        if (s[0].ToString() == "Admin")
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }


        //    }
        //    return false;
        //}


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}