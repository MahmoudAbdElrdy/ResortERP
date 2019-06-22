using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class BILL_PAY_TYPES
    {
        [Key]
        public int BillPayTypeID { get; set; }
        public int? BillMasterID { get; set; }
        public int? PayTypeID { get; set; }
        public int? AccountID { get; set; }
        public double? PayTypeValue { get; set; }
        public double? BankCommissionRate { get; set; }
        public double? BankCommissionValue { get; set; }
        public double? MaxCommission { get; set; }
        public double? CommissionTaxRate { get; set; }
        public double? CommissionTaxValue { get; set; }
    }
}
