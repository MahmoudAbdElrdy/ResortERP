using System;

namespace ResortERP.Core.VM
{
    public class VILLAGEVM
    {
        public long VILLAGE_ID { get; set; }
        public int TOWN_ID { get; set; }
        public string VILLAGE_AR_NAME { get; set; }
        public string VILLAGE_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string TownName { get; set; }
        public int? GovId { get; set; }
        public string GovName { get; set; }
        public int? NationId { get; set; }
        public string NationName { get; set; }
    }
}
