using System;

namespace ResortERP.Core.VM
{
    public class BILL_EFFECTSVM
    {
        public byte BILL_EFF_ID { get; set; }
        public string BILL_EFF_AR_NAME { get; set; }
        public string BILL_EFF_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
