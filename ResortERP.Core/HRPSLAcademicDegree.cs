using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslAcademicDegree
    {
        //public HrPslAcademicDegree()
        //{
        //    HrPslEmployeeAcademicDegree = new HashSet<HrPslEmployeeAcademicDegree>();
        //}

        public int HrPslAcademicDegreeID { get; set; }
        public string Code { get; set; }
        public string AcademicDegreeName { get; set; }
        public string AcademicDegreeNameEn { get; set; }
        public string Notes { get; set; }

        //public ICollection<HrPslEmployeeAcademicDegree> HrPslEmployeeAcademicDegree { get; set; }
    }
}
