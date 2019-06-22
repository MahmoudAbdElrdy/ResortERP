using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslContactType
    {
        //public HrPslContactType()
        //{
        //    HrPslEmployeeContacts = new HashSet<HrPslEmployeeContacts>();
        //}

        public int HrPslContactTypeID { get; set; }
        public string Code { get; set; }
        public string ContactTypeName { get; set; }
        public string ContactTypeNameEn { get; set; }
        public string Notes { get; set; }

        //public ICollection<HrPslEmployeeContacts> HrPslEmployeeContacts { get; set; }
    }
}
