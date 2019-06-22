using System;

namespace ResortERP.Core.VM
{

    public class UNITSVM
    {
        public short UNIT_ID { get; set; }
        public string UNIT_CODE { get; set; }
        public string UNIT_AR_NAME { get; set; }
        public string UNIT_EN_NAME { get; set; }
        public string UNIT_REMARKS { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }

    }

}
