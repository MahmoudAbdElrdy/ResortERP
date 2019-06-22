using System;

namespace ResortERP.Core.VM
{
    public class TStoreVM
    {
        public int TSTORE_ID { get; set; }
        public string UID { get; set; }
        public int STORE_ID { get; set; }
        public int STORE_TYPE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
