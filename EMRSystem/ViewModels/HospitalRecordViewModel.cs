using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMRSystem.ViewModels
{
    public class HospitalRecordListingViewModel
    {
        public List<HospitalRecord> HospitalRecords { get; set; }
        public string SearchTerm { get; set; }
    }

    public class HospitalRecordActionViewModel
    {
        public int ID { get; set; }
        public string HopistalID { get; set; }
        public string Disease { get; set; }
        public string Doctor { get; set; }
    }
}