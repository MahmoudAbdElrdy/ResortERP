using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class GlBank
    {
        //public GlBank()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //}

        public int GlBankID { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool? Disable { get; set; }
        public string AddressNotes { get; set; }

        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
    }
}
