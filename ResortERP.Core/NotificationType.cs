using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class NotificationType
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }
        public string Notes { get; set; }
        public string IconURL { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<Boolean> Active { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> CountryID { get; set; }
    }
}
