using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMRSystem.Entities
{
    public class Invoice:BaseEntity
    {
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string SuggestMedicine { get; set; }
        public string Attachment { get; set; }
        public string Remarks { get; set; }
    }
}
