using EMRSystem.Entities;
using EMRSystem.Services;
using EMRSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMRSystem.Controllers
{
    public class HospitalController : Controller
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
        // GET: Hospital
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            HospitalViewModel model = new HospitalViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.SignedInUser = user;
            model.Patients = SearchUsers().Where(x => x.Role == "Patient").OrderByDescending(x => x.Id)
                .ToList();

            return View(model);
        }
        public IEnumerable<User> SearchUsers()
        {
            var users = UserManager.Users.AsQueryable();
           

            return users.OrderByDescending(x => x.Id)
                .ToList();
        }


        public IEnumerable<User> SearchUsers(string Parameter,string searchTerm = "")
        {
            var users = UserManager.Users.AsQueryable();
            if (Parameter == "Name")
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    users = users.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
                }
            }
            else if(Parameter == "Email")
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
                }
            }
            else if (Parameter == "CNIC")
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    users = users.Where(a => a.CNIC.ToLower().Contains(searchTerm.ToLower()));
                }
            }
           


            return users.OrderByDescending(x => x.Id)
                .ToList();
        }





        [HttpPost]
        public ActionResult Dashboard(string Parameter,string SearchTerm="")
        {
            HospitalViewModel model = new HospitalViewModel();
            var user = UserManager.FindById(User.Identity.GetUserId());
            model.Patients = SearchUsers(Parameter, SearchTerm).Where(x => x.Role == "Patient").OrderByDescending(x => x.Id)
                .ToList();
            model.SignedInUser = user;

            return View(model);
        }

    }
}