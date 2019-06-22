using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class UNITS
    {
        [Key]
        public short UNIT_ID { get; set; }
        public string UNIT_CODE { get; set; }
        public string UNIT_AR_NAME { get; set; }
        public string UNIT_EN_NAME { get; set; }
        public string UNIT_REMARKS { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
