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
    public class HospitalRecordServices
    {
        #region Singleton
        public static HospitalRecordServices Instance
        {
            get
            {
                if (instance == null) instance = new HospitalRecordServices();
                return instance;
            }
        }
        private static HospitalRecordServices instance { get; set; }
        private HospitalRecordServices()
        {
        }
        #endregion





        public List<HospitalRecord> GetRentHospitalRecords(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.HospitalRecords.Where(p => p.Doctor != null && p.Doctor.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Doctor)
                                            .ToList();
                }
                else
                {
                    return context.HospitalRecords.OrderBy(x => x.Doctor).ToList();
                }
            }
        }

        public List<string> GetDoctorsUsingDisease(string Disease)
        {
            using (var context = new DSContext())
            {
               
                    return context.HospitalRecords.Where(X=>X.Disease == Disease).Select(X=>X.Doctor).ToList();
                
            }
        }





        public HospitalRecord GetHospitalRecord(int ID)
        {
            using (var context = new DSContext())
            {

                return context.HospitalRecords.Find(ID);

            }
        }

        public void SaveHospitalRecord(HospitalRecord HospitalRecord)
        {
            using (var context = new DSContext())
            {
                context.HospitalRecords.Add(HospitalRecord);
                context.SaveChanges();
            }
        }

        public void UpdateHospitalRecord(HospitalRecord HospitalRecord)
        {
            using (var context = new DSContext())
            {
                context.Entry(HospitalRecord).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteHospitalRecord(int ID)
        {
            using (var context = new DSContext())
            {

                var HospitalRecord = context.HospitalRecords.Find(ID);
                context.HospitalRecords.Remove(HospitalRecord);
                context.SaveChanges();
            }
        }
    }
}
