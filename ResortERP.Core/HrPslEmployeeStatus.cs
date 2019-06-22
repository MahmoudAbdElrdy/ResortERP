using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeStatus
    {
        //public HrPslEmployeeStatus()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //}

        public int HrPslEmployeeStatusID { get; set; }
        public string EmployeeStatusName { get; set; }
        public string EmployeeStatusNameEn { get; set; }
        public string EmployeeStatusCode { get; set; }
        public string Description { get; set; }

        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
    }
}
