using System;

namespace ResortERP.Core.VM
{
    public class BILL_TYPESVM
    {
        public int BILL_TYPE_ID { get; set; }
        public int BILL_EFF_ID { get; set; }
        public string BILL_TYPE_AR_NAME { get; set; }
        public string BILL_TYPE_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
