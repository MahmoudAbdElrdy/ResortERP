using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeContacts
    {
        public int HrPslEmployeeContactsID { get; set; }
        public int HrPslEmployeeID { get; set; }
        public int ContactTypeID { get; set; }
        public string ContactText { get; set; }
        public string Notes { get; set; }

        //public HrPslContactType ContactType { get; set; }
        //public HrPslEmployee HrPslEmployee { get; set; }
    }
}
