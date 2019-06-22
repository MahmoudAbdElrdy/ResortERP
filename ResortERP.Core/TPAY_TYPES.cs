using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class TPAY_TYPES
    {
        [Key]
        public int PAY_TYPE_ID { get; set; }
        public string PAY_TYPE_CODE { get; set; }
        public string PAY_TYPE_AR_NAME { get; set; }
        public string PAY_TYPE_EN_NAME { get; set; }
        public string PAY_TYPE_NOTES { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public int? AccountID { get; set; }
        public double? BankCommissionRate { get; set; }
        public double? MaxCommission { get; set; }
        public double? CommissionTaxRate { get; set; }
       
    }
}
