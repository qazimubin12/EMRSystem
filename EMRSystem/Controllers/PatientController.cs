using EMRSystem.Entities;
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

        public ActionResult GetDoctors(string disease)
        {
            // Retrieve the list of doctors based on the selected disease
            var doctors = HospitalRecordServices.Instance.GetDoctorsUsingDisease(disease);

            // Return the list of doctors as JSON
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }


        public User SearchUsers(string PatientName)
        {
            var users = UserManager.Users.AsQueryable();
            return users.Where(x=>x.Name == PatientName).FirstOrDefault();
        }

        public ActionResult Dashboard()
        {
            PatientActionViewModel model = new PatientActionViewModel();
            model.SignedInUser = UserManager.FindById(User.Identity.GetUserId());
            model.PatientInvoices = InvoiceServices.Instance.GetPatientInvoices().Where(x=>x.Patient == model.SignedInUser.Name).ToList();  
            return View(model);
        }


        [HttpGet]
        public ActionResult GenerateInvoice(string ID)
        {
            InvoiceActionViewModel model = new InvoiceActionViewModel();
            var user = UserManager.FindById(ID);
            model.PatientFull = user;
            model.HospitalFull = UserManager.FindById(User.Identity.GetUserId());
            model.Diseases = HospitalRecordServices.Instance.GetRentHospitalRecords().Where(x => x.HopistalID == User.Identity.GetUserId()).Select(X => X.Disease).Distinct().ToList();
            model.Doctors = HospitalRecordServices.Instance.GetRentHospitalRecords().Where(x => x.HopistalID == User.Identity.GetUserId()).Select(X=>X.Doctor).Distinct().ToList();
            return View(model);
        }


        [HttpPost]
        public ActionResult GenerateInvoice(InvoiceActionViewModel model)
        {
            var newInvoice = new Invoice();
            newInvoice.Attachment = model.Attachment;
            newInvoice.Patient = model.Patient;
            newInvoice.Doctor = model.Doctor;
            newInvoice.Hospital = model.Hospital;
            newInvoice.Disease = model.Disease;
           newInvoice.SuggestMedicine = model.SuggestMedicine;
            newInvoice.Remarks = model.Remarks;
            newInvoice.InvoiceNo = "INV" + DateTime.Now.ToString("ddmmhhmmss");
            newInvoice.InvoiceDate = DateTime.Now;
            InvoiceServices.Instance.SaveInvoice(newInvoice);
            return Json(new { success = true });

        }


        [HttpGet]
        public ActionResult ViewInvoice(InvoiceActionViewModel model)
        {
            model.PatientFull = SearchUsers(model.Patient);
            model.HospitalFull = UserManager.FindById(User.Identity.GetUserId());
            var LastSavedInvoice = InvoiceServices.Instance.GetPatientInvoices().OrderByDescending(X=>X.ID).FirstOrDefault();
            model.InvoiceNo = LastSavedInvoice.InvoiceNo;
            model.InvoiceDate = LastSavedInvoice.InvoiceDate;
            return View(model);
        }

        [HttpGet]
        public ActionResult ViewInvoiceNew(int ID)
        {
            InvoiceActionViewModel model = new InvoiceActionViewModel();
            var Invoice = InvoiceServices.Instance.GetInvoice(ID);
            model.InvoiceNo = Invoice.InvoiceNo;
            model.InvoiceDate = Invoice.InvoiceDate;
            model.Disease = Invoice.Disease;
            model.Doctor = Invoice.Doctor;
            model.SuggestMedicine = Invoice.SuggestMedicine;
            model.Remarks = Invoice.Remarks;
          
            model.HospitalFull = SearchUsers(Invoice.Hospital);
            model.PatientFull = SearchUsers(Invoice.Patient);
            return View("ViewInvoice",model);
        }




    }
}