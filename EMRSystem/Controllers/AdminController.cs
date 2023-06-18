using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EMRSystem.Services;
using EMRSystem.ViewModels;
using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMRSystem.Controllers
{
    public class AdminController : Controller
    {
        private AMSignInManager _signInManager;
        private AMRolesManager _rolesManager;
        private AMUserManager _userManager;
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        // GET: Admin
        public ActionResult Index()
        {
            AdminViewModel model = new AdminViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.Name = user.Name;
            return View(model);
        }


        public IEnumerable<User> SearchUsersbycnic(string CNIC)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(CNIC))
            {
                users = users.Where(a => a.CNIC == CNIC);
            }
            return users;
        }


        [HttpPost]
        public JsonResult CheckCNIC(string CNIC)
        {
            var users = SearchUsersbycnic(CNIC);
            if (users.Count() > 0)
            {
                bool isCNICExists = true;
                return Json(new { isCNICExists = isCNICExists }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool isCNICExists = false;
                return Json(new { isCNICExists = isCNICExists }, JsonRequestBehavior.AllowGet);


            }
        }
        public ActionResult Dashboard()
        {
            AdminViewModel model = new AdminViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.SignedInUser = user;
            model.Users = SearchUsers().OrderByDescending(x => x.Id)
                .ToList();
            model.Roles = RolesManager.Roles.ToList();

            model.Patients = model.Users.Where(X => X.Role == "Patient").Count();
            model.Hospitals = model.Users.Where(X => X.Role == "Hospital").Count();
            model.Admins = model.Users.Where(X => X.Role == "Admin").Count();
            return View(model);
        }

        public IEnumerable<User> SearchUsers(string searchTerm = "")
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }



            return users.OrderByDescending(x => x.Id)
                .ToList();
        }


        public IEnumerable<User> SearchUsers(string Paramter, string searchTerm = "")
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower())
                && a.Role == Paramter);
            }



            return users.Where(a => a.Role == Paramter);
        }



        [HttpPost]
        public ActionResult Dashboard(string Parameter, string SearchTerm)
        {
            AdminViewModel model = new AdminViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.SignedInUser = user;
            model.Users = SearchUsers(Parameter, SearchTerm)
                .OrderByDescending(x => x.Id)
                .ToList();
            model.Paramter = Parameter;
            model.SearchTerm = SearchTerm;
            model.Roles = RolesManager.Roles.ToList();
            model.Patients = model.Users.Where(X => X.Role == "Patient").Count();
            model.Hospitals = model.Users.Where(X => X.Role == "Hospital").Count();
            model.Admins = model.Users.Where(X => X.Role == "Admin").Count();
            return View(model);
        }


    }
}