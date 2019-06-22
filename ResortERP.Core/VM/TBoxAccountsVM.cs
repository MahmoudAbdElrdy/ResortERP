using System;

namespace ResortERP.Core.VM
{
    public class TBoxAccountsVM
    {
        public int BOXACC_ID { get; set; }
        public string UID { get; set; }
        public int ACC_ID { get; set; }
        public int BOXACC_TYPE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
