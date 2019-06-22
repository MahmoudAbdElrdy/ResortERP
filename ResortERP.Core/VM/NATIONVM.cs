using System;

namespace ResortERP.Core.VM
{
    public class NATIONVM
    {
        public short NATION_ID { get; set; }
        public string NATION_AR_NAME { get; set; }
        public string NATION_EN_NAME { get; set; }
        public string NATIONALITY_AR_NAME { get; set; }
        public string NATIONALITY_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
