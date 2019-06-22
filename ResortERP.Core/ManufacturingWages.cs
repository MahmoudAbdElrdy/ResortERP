using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class ManufacturingWages
    {
        [Key]
        public short ManufacturingWage_ID { get; set; }
        public string ManufacturingWage_CODE { get; set; }
        public string ManufacturingWage_AR_NAME { get; set; }
        public string ManufacturingWage_EN_NAME { get; set; }
        public string ManufacturingWage_REMARKS { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
