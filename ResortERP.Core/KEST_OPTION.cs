using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class KEST_OPTION
    {
        [Key]
        public int option_id { get; set; }
        public string account_name { get; set; }
        public Nullable<int> account_id { get; set; }
        public string account_code { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
