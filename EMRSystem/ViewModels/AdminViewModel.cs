using EMRSystem.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMRSystem.ViewModels
{
    public class AdminViewModel
    {
        public User SignedInUser { get; set; }
        public List<User> Users { get; set; }
        public int Admins { get; set; }
        public int Patients { get; set; }
        public int Hospitals { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string Name { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string SearchTerm { get; set; }
        public string Paramter { get; set; }
        public string CNIC { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string RegisteredNo { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
    }


    public class HospitalViewModel
    {
        public string SearchTerm { get; set; }

        public string Parameter { get; set; }
        public User SignedInUser { get; set; }
        public List<User> Patients { get; set; }
        public List<HospitalRecord> HospitalRecords { get; set; }

    }

}