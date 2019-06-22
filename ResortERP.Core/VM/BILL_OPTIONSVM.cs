using System;

namespace ResortERP.Core.VM
{
    public class BILL_OPTIONSVM
    {
        public int BILL_OPTION_ID { get; set; }
        public string BILL_OPTION_AR_NAME { get; set; }
        public string BILL_OPTION_EN_NAME { get; set; }
        public string BILL_OPTION_ABRV_AR { get; set; }
        public string BILL_OPTION_ABRV_EN { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
