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
    public class InvoiceController : Controller
    {

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
        public InvoiceController()
        {
        }



        public InvoiceController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Invoice
        public ActionResult Index(string SearchTerm = "")
        {
            InvoiceListingViewModel model = new InvoiceListingViewModel();
            var Hospital = UserManager.FindById(User.Identity.GetUserId());
            if (User.IsInRole("Admin"))
            {
                model.Invoices = InvoiceServices.Instance.GetPatientInvoices();

            }
            else
            {
                model.Invoices = InvoiceServices.Instance.GetPatientInvoices().Where(x => x.Hospital == Hospital.Name).ToList();
            }
            return View(model);
        }



        [HttpGet]
        public ActionResult ViewInvoiceNew(int ID)
        {
            HospitalInvoiceActionViewModel model = new HospitalInvoiceActionViewModel();
            var Invoice = InvoiceServices.Instance.GetInvoice(ID);
            model.InvoiceNo = Invoice.InvoiceNo;
            model.InvoiceDate = Invoice.InvoiceDate;
            model.Disease = Invoice.Disease;
            model.Doctor = Invoice.Doctor;
            model.SuggestMedicine = Invoice.SuggestMedicine;
            model.Remarks = Invoice.Remarks;

            model.HospitalFull = SearchUsers(Invoice.Hospital);
            model.PatientFull = SearchUsers(Invoice.Patient);
            return View("ViewInvoice", model);
        }



        public User SearchUsers(string PatientName)
        {
            var users = UserManager.Users.AsQueryable();
            return users.Where(x => x.Name == PatientName).FirstOrDefault();
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            HospitalInvoiceActionViewModel model = new HospitalInvoiceActionViewModel();

            var hrecord = InvoiceServices.Instance.GetInvoice(ID);
            model.ID = hrecord.ID;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public ActionResult Delete(HospitalInvoiceActionViewModel model)
        {
            InvoiceServices.Instance.DeleteInvoice(model.ID);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        
    }
}