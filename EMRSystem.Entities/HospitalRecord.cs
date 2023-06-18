using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRSystem.Entities
{
    public class HospitalRecord:BaseEntity
    {
        public string HopistalID { get; set; }
        public string Disease { get; set; }
        public string Doctor { get; set; }
    }
}
