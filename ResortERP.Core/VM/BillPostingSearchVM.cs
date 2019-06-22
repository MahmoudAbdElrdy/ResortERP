using System;
using System.Collections.Generic;

namespace ResortERP.Core.VM
{
    public class BillPostingSearchVM
    {
        public int? EmployeeId { get; set; }
        public int? CostCenterId { get; set; }
        public int? StoreId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int? BillTypeId { get; set; }
        public int? PayTypeId { get; set; }
        public int? FromBillNumber { get; set; }
        public int? ToBillNumber { get; set; }
    }
}
