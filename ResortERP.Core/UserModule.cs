using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class UserModule
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }
        public string Notes { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public string UID { get; set; }
    }
}
