using System;

namespace ResortERP.Core.VM
{
    public class Entry_TypesVM
    {
        public int ENTRY_TYPE_ID { get; set; }
        public string ENTRY_AR_NAME { get; set; }
        public string ENTRY_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
