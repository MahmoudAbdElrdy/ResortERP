using System;

namespace ResortERP.Core.VM
{
    public  class ItemsUnitsVM
    {
        public int ITEM_UNIT_ID { get; set; }
        public int ITEM_ID { get; set; }
        public int UNIT_ID { get; set; }
        public int UNIT_MASTER_UNIT_ID { get; set; }
        public double UNIT_TRANS_RATE { get; set; }
        public bool IS_DEFAULT { get; set; }
        public string UNIT_BARCODE { get; set; }
        public byte[] UNIT_BARCODE_IMAGE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string Operation { get; set; }
    }
}
