using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRSystem.Entities
{
    public class Invoice:BaseEntity
    {
        public string InvoiceNo { get; set; }
        public string Disease { get; set; }
        public string Hospital { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string SuggestMedicine { get; set; }
        public string Attachment { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Remarks { get; set; }
    }
}
