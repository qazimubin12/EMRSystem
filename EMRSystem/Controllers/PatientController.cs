using EMRSystem.Services;
using EMRSystem.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMRSystem.Controllers
{
    public class PatientController : Controller
    {

        #region Services
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
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

        private AMRolesManager _rolesManager;
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
        public PatientController()
        {
        }



        public PatientController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion
        // GET: Patient
        public ActionResult View(string ID)
        {
            PatientActionViewModel model = new PatientActionViewModel();
            var user = UserManager.FindById(ID);
            model.Gender = user.Gender;
            model.DOB = user.DOB;
            model.CNIC = user.CNIC;
            model.Age = user.Age;
            model.Name = user.Name;
            model.Contact = user.PhoneNumber;
            model.Email = user.Email;
            return View(model);
        }

        public ActionResult Dashboard()
        {
            PatientActionViewModel model = new PatientActionViewModel();
            model.SignedInUser = UserManager.FindById(User.Identity.GetUserId());
            model.PatientInvoices = InvoiceServices.Instance.GetPatientInvoices().Where(x=>x.Patient == model.SignedInUser.Name).ToList();  
            return View(model);
        }


        [HttpGet]
        public ActionResult Invoice(string ID)
        {
            return View();
        }

       
    }
}