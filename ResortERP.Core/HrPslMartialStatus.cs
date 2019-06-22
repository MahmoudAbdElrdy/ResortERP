using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslMartialStatus
    {
        public HrPslMartialStatus()
        {
            //HrPslEmployee = new HashSet<HrPslEmployee>();
        }

        public int HrPslMartialStatusID { get; set; }
        public string MartialStatusName { get; set; }
        public string MartialStatusNameEn { get; set; }
        public string MartialStatusCode { get; set; }

        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
    }
}
