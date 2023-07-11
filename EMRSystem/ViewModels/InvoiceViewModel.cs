using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMRSystem.ViewModels
{
    public class InvoiceListingViewModel
    {
        public List<Invoice> Invoices { get; set; }
        public string SearchTerm { get; set; }
    }

    public class HospitalInvoiceActionViewModel
    {
        public int ID { get; set; }
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