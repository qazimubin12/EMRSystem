using EMRSystem.Database;
using EMRSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRSystem.Services
{
    public class InvoiceServices
    {
        #region Singleton
        public static InvoiceServices Instance
        {
            get
            {
                if (instance == null) instance = new InvoiceServices();
                return instance;
            }
        }
        private static InvoiceServices instance { get; set; }
        private InvoiceServices()
        {
        }
        #endregion





        public List<Invoice> GetPatientInvoices(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.Invoices.Where(p => p.Doctor != null && p.Doctor.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Doctor)
                                            .ToList();
                }
                else
                {
                    return context.Invoices.OrderBy(x => x.Doctor).ToList();
                }
            }
        }





        public Invoice GetInvoice(int ID)
        {
            using (var context = new DSContext())
            {

                return context.Invoices.Find(ID);

            }
        }

        public void SaveInvoice(Invoice Invoice)
        {
            using (var context = new DSContext())
            {
                context.Invoices.Add(Invoice);
                context.SaveChanges();
            }
        }

        public void UpdateInvoice(Invoice Invoice)
        {
            using (var context = new DSContext())
            {
                context.Entry(Invoice).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteInvoice(int ID)
        {
            using (var context = new DSContext())
            {

                var Invoice = context.Invoices.Find(ID);
                context.Invoices.Remove(Invoice);
                context.SaveChanges();
            }
        }
    }
}
