using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortERP.Core
{
    public class SHORTCUTS
    {
        [Key, Column(Order = 1)]
        public string UID { get; set; }
        [Key, Column(Order = 2)]
        public string KEY_TYPE { get; set; }
        public short ORDER_ID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}
