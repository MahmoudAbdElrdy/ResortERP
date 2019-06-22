using System;

namespace ResortERP.Core.VM
{
    public class TelephoneTypesVM
    {
        public short TELE_TYPE_ID { get; set; }
        public string TELE_TYPE_AR_NAME { get; set; }
        public string TELE_TYPE_EN_NAME { get; set; }
        public string DATABASE_TABLE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
