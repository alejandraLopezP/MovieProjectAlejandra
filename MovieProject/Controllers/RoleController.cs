using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieProject.Models.DataBase;

namespace PracticeIdentity.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Role
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ViewBag.Roles = context.Roles.ToList();

            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewBag.Flg = 1;
                }
                else
                {
                    ViewBag.Flg = 0;
                }
            }

            return View();
        }

        //Creating Roles
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string name)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = name;

            if (!roleManager.RoleExists(name))
            {
                var result = roleManager.Create(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");//CreateRole?
                }
            }

            return View((object)name);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleToDelete = roleManager.FindById(id);

            if(roleToDelete != null)
            {
                var result = roleManager.Delete(roleToDelete);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Users(string id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role = roleManager.FindById(id);
            List<string> memberIDs = role.Users.Select(x => x.UserId).ToList(); //1,2,3

            var members = UserManager.Users.Where(x => memberIDs.Contains(x.Id));
            var noMembers = UserManager.Users.Except(members).ToList();

            ViewBag.Members = members;
            ViewBag.NonMembers = noMembers;
            ViewBag.Role = role;

            return View();
        }

        [HttpPost]
        public ActionResult AddToRole(string id, string userId, string rolename)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var result = UserManager.AddToRole(userId, rolename);

            if (!result.Succeeded)
            {

            }

            return RedirectToAction("Users", new { id = id });
        }

        [HttpPost]
        public ActionResult RemoveToRole(string id, string userId, string rolename)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var result = UserManager.RemoveFromRole(userId, rolename);

            if (!result.Succeeded)
            {

            }

            return RedirectToAction("Users", new { id = id });
        }

    }
}