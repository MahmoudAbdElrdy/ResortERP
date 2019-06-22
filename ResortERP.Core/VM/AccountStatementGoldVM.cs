using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class AccountStatementGoldVM : AccountStatementVM
    {
        public double? DEBIT_Gold24 { get; set; }

        public double? CREDIT_Gold24 { get; set; }

        public double? BAL_Gold24 { get; set; }



        public double? DEBIT_Gold22 { get; set; }

        public double? CREDIT_Gold22 { get; set; }

        public double? BAL_Gold22 { get; set; }



        public double? DEBIT_Gold21 { get; set; }

        public double? CREDIT_Gold21 { get; set; }

        public double? BAL_Gold21 { get; set; }



        public double? DEBIT_Gold18 { get; set; }

        public double? CREDIT_Gold18 { get; set; }

        public double? BAL_Gold18 { get; set; }
    }

    public class TrialBalance
    {

        public int? ACID { get; set; }
        public int? PARENT_ID { get; set; }

        public string AC_CODE { get; set; }

        public string AC_NAME { get; set; }

        public int? LEVEL { get; set; }

        public double? BDEBIT { get; set; }

        public double? BCREDIT { get; set; }

        public double? INDEBIT { get; set; }

        public double? INCREDIT { get; set; }

        public double? ADEBIT { get; set; }

        public double? ACREDIT { get; set; }
    }
}
