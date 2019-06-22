using System;

namespace ResortERP.Core.VM
{
    public class TelephoneCatVM
    {
        public short TELE_CAT_ID { get; set; }
        public string TELE_CAT_AR_NAME { get; set; }
        public string TELE_CAT_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
