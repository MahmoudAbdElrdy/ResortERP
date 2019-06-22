using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class AccountStatementMonthlyVM
    {

        public double CREDIDT { get; set; }

        public double DEBIT { get; set; }

        public double BAL { get; set; }

        public string CHEET { get; set; }

        public string ACCOUNT { get; set; }

        public int? BILL_ID { get; set; }

        public int? ENTRY_ID { get; set; }

        public string DATE { get; set; }

        public string NOTE { get; set; }

        public string BILL_AR_NAME { get; set; }

        public int? BILL_NUMBER { get; set; }

        public int? ENTRY_NUMBER { get; set; }

        public string BILL_REMARKS { get; set; }

    }
}
