using System;
using System.Collections.Generic;

namespace ResortERP.Core.VM
{
    public class InvoiceMotionSearchModel
    {
        public int? EmployeeID { get; set; }
        public int? CostCenterID { get; set; }
        public int? finalize { get; set; }
        public int? CustID { get; set; }
        public int? payTypeId { get; set; }
        public int? storeID { get; set; }
        public int? CurrencyId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string companyBranches { get; set; }
        public int[] bilSettings { get; set; }
        public int[] rptOptions { get; set; }
    }
}
