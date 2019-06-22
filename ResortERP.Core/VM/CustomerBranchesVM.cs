using System;

namespace ResortERP.Core.VM
{
    public class CustomerBranchesVM
    {
        public int CUST_BRN_ID { get; set; }
        public int ACC_ID { get; set; }
        public string ACC_BRN_AR_NAME { get; set; }
        public string ACC_BRN_EN_NAME { get; set; }
        public short NATION_ID { get; set; }
        public short GOV_ID { get; set; }
        public Nullable<int> TOWN_ID { get; set; }
        public Nullable<long> VILLAGE_ID { get; set; }
        public string BRN_ADDR_REMARKS { get; set; }
        public string BRN_REMARKS { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        
    }
}
