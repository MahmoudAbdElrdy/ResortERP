using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class Income_Account_Types
    {
        [Key]
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> disable { get; set; }
    }
}
