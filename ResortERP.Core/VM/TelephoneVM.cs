using System;

namespace ResortERP.Core.VM
{
    public class TelephoneVM
    {
        public int TELE_ID { get; set; }
        public short TELE_TYPE_ID { get; set; }
        public string TELE_NUMBER { get; set; }
        public short TELE_CAT_ID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public string TeleCatName { get; set; }
    }
}
