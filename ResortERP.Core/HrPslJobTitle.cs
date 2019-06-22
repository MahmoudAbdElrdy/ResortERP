using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslJobTitle
    {
        //public HrPslJobTitle()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //    HrRecJobQualificationHrPslJobTitle = new HashSet<HrRecJobQualificationHrPslJobTitle>();
        //}

        public int HrPslJobTitleID { get; set; }
        public string JobTitleName { get; set; }
        public string JobTitleNameEn { get; set; }
        public string JobTitleCode { get; set; }

        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
        //public ICollection<HrRecJobQualificationHrPslJobTitle> HrRecJobQualificationHrPslJobTitle { get; set; }
    }
}
