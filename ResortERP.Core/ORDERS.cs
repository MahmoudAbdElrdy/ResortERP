using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class ORDERS
    {
        [Key]
        public short ORDER_ID { get; set; }
        public string ORDER_AR_NAME { get; set; }
        public string ORDER_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
    }
}
