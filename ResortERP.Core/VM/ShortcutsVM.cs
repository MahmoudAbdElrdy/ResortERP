using System;

namespace ResortERP.Core.VM
{
    public class ShortcutsVM
    {
        public string UID { get; set; }
        public string KEY_TYPE { get; set; }
        public short ORDER_ID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
