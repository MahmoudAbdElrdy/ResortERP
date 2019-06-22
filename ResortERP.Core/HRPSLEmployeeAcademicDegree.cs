using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeAcademicDegree
    {
        public int HrPslEmployeeAcademicDegreeID { get; set; }
        public int HrPslEmployeeID { get; set; }
        public int AcademicDegreeID { get; set; }
        public DateTime DegreeDate { get; set; }
        public string University { get; set; }
        public string Major { get; set; }
        public string Grade { get; set; }
        public string Notes { get; set; }

        //public HrPslAcademicDegree AcademicDegree { get; set; }
        //public HrPslEmployee HrPslEmployee { get; set; }
    }
}
