using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslReligion
    {
        //public HrPslReligion()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //}

        public int HrPslReligionID { get; set; }
        public string ReligionCode { get; set; }
        public string ReligionName { get; set; }
        public string ReligionNameEng { get; set; }

        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
    }
}
