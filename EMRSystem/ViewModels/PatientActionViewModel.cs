using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMRSystem.ViewModels
{
    public class PatientActionViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

        public string CNIC { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public List<Invoice> PatientInvoices { get; set; }
        public User SignedInUser { get; set; }
    }


    public class InvoiceActionViewModel
    {
        public User HospitalFull { get; set; }
        public string InvoiceNo { get; set; }
        public List<string> Doctors { get; set; }
        public List<string> Diseases { get; set; }
        public string Disease { get; set; }
        public User PatientFull { get; set; }
        public string Hospital { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string SuggestMedicine { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Attachment { get; set; }
        public string Remarks { get; set; }
    }
}