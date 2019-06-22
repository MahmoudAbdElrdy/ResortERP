using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeJobLevel
    {
        //public HrPslEmployeeJobLevel()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //}

        public int HrPslEmployeeJobLevelID { get; set; }
        public string EmployeeJobLevelCode { get; set; }
        public string EmployeeJobLevelName { get; set; }
        public string EmployeeJobLevelNameEn { get; set; }
        public int? FunctionalClass { get; set; }

        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
    }
}
