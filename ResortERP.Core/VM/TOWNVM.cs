using System;

namespace ResortERP.Core.VM
{
    public class TOWNVM
    {
        public int TOWN_ID { get; set; }
        public short GOV_ID { get; set; }
        public string TOWN_AR_NAME { get; set; }
        public string TOWN_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string GovName { get; set; }
        public int? NationId { get; set; }
        public string NationName { get; set; }
    }
}
