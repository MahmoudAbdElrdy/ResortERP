using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolversTeamERP.Core.VM
{
    public class AccountBalancesReportSearchVM
    {
        public int CostCenterId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int CurrencyId { get; set; }
        public string Accounts { get; set; }
        public int ReportType { get; set; }
        public int[] ReportSources { get; set; }
        public int Number { get; set; }
    }
}
