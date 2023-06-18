using Microsoft.AspNet.Identity.EntityFramework;
using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRSystem.Database
{
    public class DSContext :IdentityDbContext<User>,IDisposable
    {
        public DSContext() : base("EMRConnectionStrings")
        {

        }

        public static DSContext Create()
        {
            return new DSContext();
        }

        public DbSet<HospitalRecord> HospitalRecords { get; set; }

        public DbSet<Invoice> Invoices { get; set; }



    }
}
