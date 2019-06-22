using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class VILLAGES
    {
        [Key]
        public long VILLAGE_ID { get; set; }
        public int TOWN_ID { get; set; }
        public string VILLAGE_AR_NAME { get; set; }
        public string VILLAGE_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
