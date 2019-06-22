using System;

namespace ResortERP.Core.VM
{
    public class AccountTypeVM
    {
        public byte ACC_TYPE_ID { get; set; }
        public string ACC_TYPE_AR_NAME { get; set; }
        public string ACC_TYPE_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> disable { get; set; }
    }
}
