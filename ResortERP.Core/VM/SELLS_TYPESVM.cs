using System;

namespace ResortERP.Core.VM
{
    public  class SELLS_TYPESVM
    {
        public byte SELL_TYPE_ID { get; set; }
        public string SELL_TYPE_AR_NAME { get; set; }
        public string SELL_TYPE_EN_NAME { get; set; }
        public string SHOW_OR_NOT { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
