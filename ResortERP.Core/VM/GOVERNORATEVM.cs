using System;

namespace ResortERP.Core.VM
{
    public class GOVERNORATEVM
    {
        public short GOV_ID { get; set; }
        public short NATION_ID { get; set; }
        public string GOV_AR_NAME { get; set; }
        public string GOV_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string NationName { get; set; }
    }
}
