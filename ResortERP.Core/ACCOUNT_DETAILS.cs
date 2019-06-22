using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortERP.Core
{
    public class ACCOUNT_DETAILS
    {
        [Key, Column(Order = 0)]
        public int PARENT_ACC_ID { get; set; }
        [Key, Column(Order = 1)]
        public int CHILD_ACC_ID { get; set; }
        public double PARTITIONING_FACTOR { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
