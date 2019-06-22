using System;

namespace ResortERP.Core.VM
{
    public class SearchingmainItems
    {
        public int? itemID { get; set; }
        public string itemName { get; set; }
        public int? itemUnitID { get; set; }
        public int? storeID { get; set; }
        public string storeName { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public CurrencyVM Currency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CostCenterID { get; set; }
        public string CostCenterName { get; set; }
        public int? finalize { get; set; }
        public int? CustID { get; set; }
        public int? payTypeId { get; set; }
        public string companyBranches { get; set; }
    }
}
