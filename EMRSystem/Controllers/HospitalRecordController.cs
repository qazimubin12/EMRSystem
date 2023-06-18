using EMRSystem.Entities;
using EMRSystem.Services;
using EMRSystem.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMRSystem.Controllers
{
    public class HospitalRecordController : Controller
    {
        // GET: HospitalRecord
        public ActionResult Index(string SearchTerm = "")
        {
            HospitalRecordListingViewModel model = new HospitalRecordListingViewModel();
            model.HospitalRecords = HospitalRecordServices.Instance.GetRentHospitalRecords(SearchTerm);
            model.SearchTerm = SearchTerm;
            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            HospitalRecordActionViewModel model = new HospitalRecordActionViewModel();
            if(model.ID != 0)
            {
                var hospitalrecord = HospitalRecordServices.Instance.GetHospitalRecord(ID);
                model.ID = hospitalrecord.ID;
                model.Doctor = hospitalrecord.Doctor;
                model.Disease = hospitalrecord.Disease;
                
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Action(HospitalRecordActionViewModel model)
        {
            if(model.ID != 0)
            {
                var hospitalrecord = HospitalRecordServices.Instance.GetHospitalRecord(model.ID);
                hospitalrecord.ID = model.ID;
                hospitalrecord.Doctor = model.Doctor;
                hospitalrecord.Disease = model.Disease;
                HospitalRecordServices.Instance.UpdateHospitalRecord(hospitalrecord);
            }
            else
            {
                var hospitalrecord = new HospitalRecord();
                hospitalrecord.Doctor = model.Doctor;
                hospitalrecord.Disease = model.Disease;
                hospitalrecord.HopistalID = User.Identity.GetUserId();
                HospitalRecordServices.Instance.UpdateHospitalRecord(hospitalrecord);
            }
            return Json(new {success=true},JsonRequestBehavior.AllowGet);   
        }


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            HospitalRecordActionViewModel model = new HospitalRecordActionViewModel();

            var hrecord = HospitalRecordServices.Instance.GetHospitalRecord(ID);
            model.ID = hrecord.ID;

            return PartialView("_Delete", model);
        }


        [HttpPost]
        public ActionResult Delete(HospitalRecordActionViewModel model)
        {
            HospitalRecordServices.Instance.DeleteHospitalRecord(model.ID);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}