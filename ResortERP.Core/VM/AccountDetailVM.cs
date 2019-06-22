using System;

namespace ResortERP.Core.VM
{
    public class AccountDetailVM
    {
        public int PARENT_ACC_ID { get; set; }
        public int CHILD_ACC_ID { get; set; }
        public double PARTITIONING_FACTOR { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
