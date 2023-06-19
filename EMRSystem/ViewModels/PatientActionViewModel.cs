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
}