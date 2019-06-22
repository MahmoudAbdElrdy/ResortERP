using System;
using System.Collections.Generic;

namespace ResortERP.Core.VM
{
    public class EntryPostingSearchVM
    {
        public int? EmployeeId { get; set; }
        public int? CostCenterId { get; set; }
        public int? BoxAccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? EntryTypeId { get; set; }
        public int? FromEntryNumber { get; set; }
        public int? ToEntryNumber { get; set; }
    }
}
