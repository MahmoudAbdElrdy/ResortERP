using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeFamilyDetails
    {
        public int HrPslEmployeeFamilyDetailsID { get; set; }
        public int HrPslEmployeeID { get; set; }
        public int? RelationshipType { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }

        //public HrPslEmployee HrPslEmployee { get; set; }
    }
}
