using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeExperience
    {
        public int HrPslEmployeeExperienceID { get; set; }
        public int HrPslEmployeeID { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Notes { get; set; }

        //public HrPslEmployee HrPslEmployee { get; set; }
    }
}
