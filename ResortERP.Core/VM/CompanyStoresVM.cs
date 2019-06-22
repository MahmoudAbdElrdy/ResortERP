using System;

namespace ResortERP.Core.VM
{
    public class CompanyStoresVM
    {
        public short COM_STORE_ID { get; set; }
        public string COM_STORE_CODE { get; set; }
        public string COM_STORE_AR_NAME { get; set; }
        public string COM_STORE_EN_NAME { get; set; }
        public string COM_STORE_AR_ABRV { get; set; }
        public string COM_STORE_EN_ABRV { get; set; }
        public short NATION_ID { get; set; }
        public short GOV_ID { get; set; }
        public Nullable<int> TOWN_ID { get; set; }
        public Nullable<long> VILLAGE_ID { get; set; }
        public string COM_STORE_ADDR_REMARKS { get; set; }
        public Nullable<short> COM_MASTER_STORE_ID { get; set; }
        public string COM_STORE_REMARKS { get; set; }
        public Nullable<int> COM_BRN_ID { get; set; }
        public Nullable<int> EMP_ID { get; set; }
        public string COM_PRINTER_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string StoreManagerNameAr { get; set; }
        public string CompanyBranchAR { get; set; }
        public string CompanyMainStore { get; set; }

    }
}
