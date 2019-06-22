using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolversTeamERP.Core.VM
{
    public class AccountBalancesReportResultVM
    {
        public List<AccountBalancesTransactionsVM> Transactions { get; set; }
        public List<AccountBalancesPreviousBalanceVM> PreviousBalance { get; set; }
        public List<AccountBalancesLastTransactionsVM> LastTransactions { get; set; }
        public double TotalCredit { get; set; }
        public double TotalDebit { get; set; }
        public double TotalBalance { get; set; }
        public double TotalPrevCredit { get; set; }
        public double TotalPrevDebit { get; set; }
        public double TotalPrevBalance { get; set; }
    }

    public class AccountBalancesTransactionsVM
    {
        public double? CREDIT { get; set; }
        public double? DEBIT { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_AR_NAME { get; set; }
        public string ACC_EN_NAME { get; set; }
        public int NUMBER { get; set; }
        public int ACC_ID { get; set; }
        public double? Balance { get; set; }
        public double? LAST_CREDIT { get; set; }
        public double? LAST_DEBIT { get; set; }
        public double? PreviousDebit { get; set; }
        public double? PreviousCredit { get; set; }
        public double? PreviousBalance { get; set; }
    }

    public class AccountBalancesPreviousBalanceVM
    {
        public int ACC_ID { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_AR_NAME { get; set; }
        public string ACC_EN_NAME { get; set; }
        public string ACC_CUSTOMER_CODE { get; set; }        
    }

    public class AccountBalancesLastTransactionsVM
    {
        public int ACC_ID { get; set; }
        public double? LAST_CREDIT { get; set; }
        public DateTime? LAST_CREDIT_DATE { get; set; }
        public double? LAST_DEBIT { get; set; }
        public DateTime? LAST_DEBIT_DATE { get; set; }
    }
}
